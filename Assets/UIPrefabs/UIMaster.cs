using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public Boolean responsive;
    
    private GameObject verticalLayoutGroup;


    public void AppQuit()
    {
        Application.Quit();
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
            
        SetLayoutGroupSize(responsive);
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
            rectTransform.offsetMax = Vector2.up*1000 + Vector2.right*200;
            rectTransform.anchoredPosition = Vector2.zero;
        }
    }

    private void OnRectTransformDimensionsChange()
    {
        Debug.Log("Canvas Size Changed");
        if (verticalLayoutGroup != null)
        {
            SetLayoutGroupSize(responsive);
        }
    }
}