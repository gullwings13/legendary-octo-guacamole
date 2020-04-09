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
    public GameObject tankPrefab;
    public GameObject palmPrefab;
    private const float range = 10;
    
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
            AddTank();
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
            AddPalm();
        }
    }


    private void AddTank()
    {
        Vector3 pos = new Vector3(UnityEngine.Random.Range(-10,10), 0, UnityEngine.Random.Range(-10,10));
        Instantiate(tankPrefab, pos, Quaternion.identity);
        
    }

    private void AddPalm()
    {
        EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(palmPrefab, settings);
        var instance = manager.Instantiate(prefab);
        var position = transform.TransformPoint(new float3(UnityEngine.Random.Range(-range, range), 0, UnityEngine.Random.Range(-range, range)));
        manager.SetComponentData(instance, new Translation {Value = position});
        manager.SetComponentData(instance, new Rotation {Value = new quaternion(0,0,0,0)});
        
        // EntityManager manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        // var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        // var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(palmPrefab, settings);
        //
        //
        //     var instance = manager.Instantiate(prefab);
        //     var position = transform.TransformPoint(new float3(UnityEngine.Random.Range(-range, range), UnityEngine.Random.Range(0, range*2), UnityEngine.Random.Range(-range, range)));
        //     manager.SetComponentData(instance, new Translation {Value = position});
        //     manager.SetComponentData(instance, new Rotation {Value = new quaternion(0,0,0,0)});
        
    }
}
