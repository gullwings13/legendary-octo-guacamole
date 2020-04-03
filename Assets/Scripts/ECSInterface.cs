using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSInterface : MonoBehaviour
{
    private World world;
    
    public GameObject sheepPrefab;
    private const float range = 40;
    
    private void Start()
    {
        world = World.DefaultGameObjectInjectionWorld;
        Debug.Log("All Entities: "+ world.GetExistingSystem<MoveSystem>().EntityManager.GetAllEntities().Length);
        

        EntityManager entityManager = world.GetExistingSystem<MoveSystem>().EntityManager;
        EntityQuery entityQuery = entityManager.CreateEntityQuery(ComponentType.ReadOnly<SheepData>());
        
        Debug.Log("Sheep Entities: "+ entityQuery.CalculateEntityCount());
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddSheep();
        }
    }


    private void AddSheep()
    {
        EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(sheepPrefab, settings);
        var instance = manager.Instantiate(prefab);
        var position = transform.TransformPoint(new float3(UnityEngine.Random.Range(-range, range), UnityEngine.Random.Range(0, range*2), UnityEngine.Random.Range(-range, range)));
        manager.SetComponentData(instance, new Translation {Value = position});
        manager.SetComponentData(instance, new Rotation {Value = new quaternion(0,0,0,0)});
    }
}
