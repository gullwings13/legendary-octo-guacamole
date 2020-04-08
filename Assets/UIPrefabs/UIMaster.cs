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
        verticalLayoutGroup.transform.parent = mainCanvas.transform;
        var vertGroup = verticalLayoutGroup.GetComponent<VerticalLayoutGroup>();
        vertGroup.childControlHeight = true;
        vertGroup.childControlWidth = true;
        vertGroup.childForceExpandHeight = true;
        vertGroup.childForceExpandWidth = true;

        var rectTransform = verticalLayoutGroup.GetComponent<RectTransform>();

        rectTransform.localScale = Vector3.one;

        // rectTransform.offsetMin = new Vector2(0, 0); 
        // // new Vector2(left, bottom);
        // rectTransform.offsetMax = new Vector2(-0, -0);
        // // new Vector2(-right, -top);

        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;


        rectTransform.pivot = Vector2.zero;
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;

        newButton = Instantiate(buttonPrefab, verticalLayoutGroup.transform).gameObject;
        newButtonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
        newButtonText.SetText("Button text here");
    }

    private void Update()
    {
        // Vector3 pos = newButton.transform.position;
        // pos.x -= 10f;
        // newButton.transform.position = pos;
    }
}