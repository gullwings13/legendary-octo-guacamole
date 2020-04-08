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
    public Button buttonPrefab;

    private GameObject newButton;
    private TextMeshProUGUI newButtonText;

    private GameObject verticalLayoutGroup;

    private void Start()
    {
        verticalLayoutGroup = new GameObject("VerticalLayoutGroup", typeof(VerticalLayoutGroup));
        verticalLayoutGroup.transform.SetParent(mainCanvas.transform);
        var vertGroup = verticalLayoutGroup.GetComponent<VerticalLayoutGroup>();
        vertGroup.childControlHeight = true;
        vertGroup.childControlWidth = true;
        vertGroup.childForceExpandHeight = true;
        vertGroup.childForceExpandWidth = true;
        vertGroup.padding.Add(new Rect(
            new Vector2(10, 10),
            new Vector2(10, 10)));

        SetLayoutGroupSize();

        for (int i = 0; i < 5; i++)
        {
            newButton = Instantiate(buttonPrefab, verticalLayoutGroup.transform).gameObject;
            newButtonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            newButtonText.SetText("Button text here");
        }
    }


    private void SetLayoutGroupSize()
    {
        var rectTransform = verticalLayoutGroup.GetComponent<RectTransform>();

        rectTransform.localScale = Vector3.one;

        // rectTransform.offsetMin = new Vector2(0, 0); 
        // // new Vector2(left, bottom);
        // rectTransform.offsetMax = new Vector2(-0, -0);
        // // new Vector2(-right, -top);

        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        rectTransform.pivot = Vector2.zero;


        var mainCanvasRect = mainCanvas.GetComponent<RectTransform>().rect;

        float rectxMin = 0;
        float rectyMin = 0.5f;
        float rectxMax = 1;
        float rectyMax = 1;

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

        // X min, Y Min
        rectTransform.anchorMin = new Vector2(rectxMin, rectyMin);
        // X Max, Y Max
        rectTransform.anchorMax = new Vector2(rectxMax, rectyMax);
    }

    private void OnRectTransformDimensionsChange()
    {
        Debug.Log("Canvas Size Changed");
        SetLayoutGroupSize();
    }

    private void Update()
    {
        // Vector3 pos = newButton.transform.position;
        // pos.x -= 10f;
        // newButton.transform.position = pos;
    }
}