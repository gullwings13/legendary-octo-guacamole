﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{

    public float rotateSpeed;
    void Update()
    {
        transform.Rotate(Vector3.up,Time.deltaTime * rotateSpeed);
    }
}
