using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UIMaster : MonoBehaviour
{
    public Canvas mainCanvas;
    public GameObject verticalLayoutGroupPrefab;
    public Button verticalButtonPrefab;
    public Button horizontalButtonPrefab;
    public GameObject sliderInPanelPrefab;
    public GameObject verticalPanel;
    public GameObject horizontalPanel;
    public Boolean randomColor;
    public TextMeshProUGUI sliderText;

    private GameObject verticalLayoutGroup;

    public GameObject[] uiElementColorArray;

    public void AppQuit()
    {
        Application.Quit();
    }

    private void Awake()
    {
        ColorizeGameObjectArray();
    }

    [EasyButtons.Button]
    private void ColorizeGameObjectArray()
    {
        for (int i = 0; i < uiElementColorArray.Length; i++)
        {
            float colorKey = 1f - 1f / (uiElementColorArray.Length+4) * (i+2) / 1.1f + 0.1f;
            float colorAdjust = 0.01f;
            Color color1;
            Color color2;
            Color color3normal;
            Color color3hover;
            Color color3click;
            Color color4;
            Color color5;

            if (randomColor)
            {
                // See through background 
                color1 = Color.HSVToRGB(check(colorKey), 0.5f, 0.9f);
                color1.a = 1f;

                color2 = Color.HSVToRGB(check(colorKey+RandFloat()+colorAdjust*RandFloatLarge()), 0.5f, 0.5f);
                color2.a = 0.25f;

                color3normal = Color.HSVToRGB(check(colorKey+RandFloat()+colorAdjust*RandFloatLarge()), 0.5f, 0.5f);
                color3normal.a = 1f;
                color3hover = Color.HSVToRGB(check(colorKey+RandFloat()+colorAdjust*RandFloatLarge()), 0.7f, 0.7f);
                color3click = Color.HSVToRGB(check(colorKey+RandFloat()+colorAdjust*RandFloatLarge()), 0.8f, 0.8f);
                
                color4 = Color.HSVToRGB(check(colorKey+RandFloat()+colorAdjust*RandFloatLarge()), 0.9f, 0.9f);
                color4.a = 0.9f;

                color5 = Color.HSVToRGB(check(colorKey+RandFloat()+colorAdjust*RandFloatLarge()), 1, 1);
                color5.a = 0.7f;
            }
            else
            {
                // See through background 
                color1 = Color.HSVToRGB(colorKey, 0.5f, 0.9f);
                color1.a = 1f;

                color2 = Color.HSVToRGB(colorKey+colorAdjust, 0.5f, 0.5f);
                color2.a = 0.25f;

                color3normal = Color.HSVToRGB(colorKey+colorAdjust*3, 0.5f, 0.5f);
                color3normal.a = 1f;
                color3hover = Color.HSVToRGB(colorKey+colorAdjust*4, 0.7f, 0.7f);
                color3click = Color.HSVToRGB(colorKey+colorAdjust*5, 0.8f, 0.8f);
                
                color4 = Color.HSVToRGB(colorKey+colorAdjust*6, 0.9f, 0.9f);
                color4.a = 0.9f;

                color5 = Color.HSVToRGB(colorKey+colorAdjust*3, 1, 1);
                color5.a = 0.7f;
            }

            foreach (TextMeshProUGUI text in uiElementColorArray[i].GetComponentsInChildren<TextMeshProUGUI>())
            {
                text.color = color1;
            }

            uiElementColorArray[i].GetComponent<Image>().color = color2;

            var slider = uiElementColorArray[i].GetComponentInChildren<Slider>();
            if (slider != null)
            {
                var grandChildImages = slider.GetComponentsInChildren<Image>();
                foreach (Image grandChildImage in grandChildImages)
                {
                    grandChildImage.color = color4;
                    if (grandChildImage.name == "Handle")
                    {
                        grandChildImage.color = color5;
                    }
                }


                var sliderChildCount = slider.transform.childCount;
                for (int j = 0; j < sliderChildCount; j++)
                {
                    var childImage = slider.transform.GetChild(j).GetComponent<Image>();
                    if (childImage != null)
                    {
                        childImage.color = color3normal;
                    }
                }
            }

            var button = uiElementColorArray[i].GetComponentsInChildren<Button>();
            if (button != null)
            {
                foreach (Button but in button)
                {
                    var tempColors = but.colors;
                    tempColors.normalColor = color3normal;
                    tempColors.highlightedColor = color3hover;
                    tempColors.selectedColor = color3hover;
                    tempColors.pressedColor = color3click;
                    tempColors.fadeDuration = 0.01f;
                    but.colors = tempColors;
                }
            }
        }
    }

    private float RandFloat()
    {
        // return random float for adjusting color
        return UnityEngine.Random.Range(-0.03f, 0.03f);
    }
    
    private float RandFloatLarge()
    {
        // return random float for adjusting color
        return UnityEngine.Random.Range(1f, 5f);
    }

    private float check(float numberToCheckBoundsFor)
    {
        // check bound to make sure it is great than = to 0 and less than equal to 1;
        while (numberToCheckBoundsFor > 1 || numberToCheckBoundsFor < 0)
        {
            if (numberToCheckBoundsFor > 1)
            {
                numberToCheckBoundsFor = numberToCheckBoundsFor - 1;
            }
            else if (numberToCheckBoundsFor < 0)
            {
                numberToCheckBoundsFor = numberToCheckBoundsFor + 1;
            }
        }

        return numberToCheckBoundsFor;
    }



    [EasyButtons.Button]
    private void InitVertGroup()
    {
        SetupLayoutGroup();
    }

    [EasyButtons.Button]
    private void BuildButtonGUI()
    {
        BuildButton(verticalLayoutGroup, "Button Test");
    }

    [EasyButtons.Button]
    private void BuildSliderGUI()
    {
        BuildSlider(verticalLayoutGroup, "Slider Test");
    }

    [EasyButtons.Button]
    private void DestroyVertGroup()
    {
        DestroyUI();
    }

    private void SetupLayoutGroup()
    {
        // verticalLayoutGroup = new GameObject("VerticalLayoutGroup", typeof(VerticalLayoutGroup));
        verticalLayoutGroup = Instantiate(verticalLayoutGroupPrefab, mainCanvas.transform);
        // verticalLayoutGroup.transform.SetParent(mainCanvas.transform);
        var vertGroupVLG = verticalLayoutGroup.GetComponent<VerticalLayoutGroup>();
        vertGroupVLG.childControlHeight = false;
        vertGroupVLG.childControlWidth = false;
        vertGroupVLG.childForceExpandHeight = false;
        vertGroupVLG.childForceExpandWidth = false;
        vertGroupVLG.padding = new RectOffset(10, 10, 10, 10);
        vertGroupVLG.childAlignment = TextAnchor.UpperCenter;
        vertGroupVLG.spacing = 10;

        SetLayoutGroupSize(false);
    }

    private void BuildButton(GameObject verticalLayoutGroup, string buttonString)
    {
        GameObject newButton = Instantiate(verticalButtonPrefab, verticalLayoutGroup.transform).gameObject;
        TextMeshProUGUI newButtonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        newButtonText.SetText(buttonString);
    }

    private void BuildSlider(GameObject verticalLayoutGroup, string sliderLabelString)
    {
        GameObject newSlider = Instantiate(sliderInPanelPrefab, verticalLayoutGroup.transform).gameObject;
        TextMeshProUGUI newSliderText = newSlider.GetComponentInChildren<TextMeshProUGUI>();
        newSliderText.SetText(sliderLabelString);
    }

    private void DestroyUI()
    {
        GameObject.DestroyImmediate(verticalLayoutGroup.gameObject);
    }

    private void SetLayoutGroupSize(Boolean responsive)
    {
        var rectTransform = verticalLayoutGroup.GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one;

        var mainCanvasRect = mainCanvas.GetComponent<RectTransform>().rect;

        float rectxMin = 0;
        float rectyMin = 0.5f;
        float rectxMax = 1;
        float rectyMax = 1;

        if (responsive)
        {
            if (mainCanvasRect.width < 800)
            {
                rectxMax = 1f / 2f;
            }
            else if (mainCanvasRect.width < 1200)
            {
                rectxMax = 1f / 3f;
            }
            else
            {
                rectxMax = 1f / 4f;
            }

            rectTransform.anchorMin = new Vector2(rectxMin, rectyMin);
            rectTransform.anchorMax = new Vector2(rectxMax, rectyMax);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.zero;
            rectTransform.pivot = Vector2.zero;
        }
        else
        {
            // Top Left
            rectxMin = 0f;
            rectyMin = 1f;
            rectxMax = 0f;
            rectyMax = 1f;
            rectTransform.pivot = Vector2.up;
            rectTransform.anchorMin = new Vector2(rectxMin, rectyMin);
            rectTransform.anchorMax = new Vector2(rectxMax, rectyMax);
            rectTransform.offsetMin = Vector2.zero;
            rectTransform.offsetMax = Vector2.up * 1000 + Vector2.right * 200;
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    private void OnRectTransformDimensionsChange()
    {
        if (verticalLayoutGroup != null)
        {
            SetLayoutGroupSize(false);
        }
    }
}