using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Rendering;

public class ECSManager : MonoBehaviour
{
    private EntityManager manager;
    public GameObject sheepPrefab;
    private const int numSheep = 105000;
    private const float range = 40;
    private void Start()
    {
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(sheepPrefab, settings);

        for (int i = 0; i < numSheep; i++)
        {
            var instance = manager.Instantiate(prefab);
            var position = transform.TransformPoint(new float3(UnityEngine.Random.Range(-range, range), UnityEngine.Random.Range(0, range*2), UnityEngine.Random.Range(-range, range)));
            manager.SetComponentData(instance, new Translation {Value = position});
            manager.SetComponentData(instance, new Rotation {Value = new quaternion(0,0,0,0)});
        }
    }
}
