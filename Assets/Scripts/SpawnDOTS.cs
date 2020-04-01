using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Jobs;
using Unity.Jobs;


public class SpawnDOTS : MonoBehaviour
{
    public GameObject sheepPrefab;

    private const int numberToSpawn = 20000;
    private const float range = 40;
    private GameObject[] sheep = new GameObject[numberToSpawn];
    private void Start()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            var pos = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));
            sheep[i] =  Instantiate(sheepPrefab, pos, Quaternion.identity);
            
        }
    }

    private void Update()
    {
        for (int i = 0; i < numberToSpawn; i++)
        {
            sheep[i].transform.Translate(0,0,0.1f);
            if (sheep[i].transform.position.z > 40)
            {
                sheep[i].transform.position = new Vector3(sheep[i].transform.position.x,sheep[i].transform.position.y,-40);
            }
        }
    }
}
