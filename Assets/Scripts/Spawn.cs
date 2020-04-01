using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Spawn : MonoBehaviour
{
    public GameObject sheepPrefab;

    private const int numberToSpawn = 20000;
    private const float range = 40;
    private void Start()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            var pos = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
            Instantiate(sheepPrefab, pos, Quaternion.identity);
        }
    }
}
