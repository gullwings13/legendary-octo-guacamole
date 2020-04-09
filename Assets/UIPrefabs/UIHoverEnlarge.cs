using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverEnlarge : MonoBehaviour
{
    public void EnlargeScale()
    {
        transform.localScale = Vector3.one * 1.1f;
    }

    public void ResetScale()
    {
        transform.localScale = Vector3.one;
    }
}