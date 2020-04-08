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

    private void Start()
    {
       var newButton = Instantiate(buttonPrefab, mainCanvas.transform);
       var newButtonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
       newButtonText.SetText("Button text here");
       Vector3 pos = newButton.transform.position;
       pos.x -= 10f;
       newButton.transform.position = pos;
    }


    private void Update()
    {
        Vector3 pos = newButton.transform.position;
        pos.x -= 10f;
        newButton.transform.position = pos;
    }
}
