using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class ECSManager : MonoBehaviour
{
    EntityManager manager;
    public GameObject asteroidPrefab;
    private GameObjectConversionSettings settings;
    private Entity convertedPrefab;
    public const int initialNumberOfAsteroids = 1;
    public int amount;
    

    public void Add()
    {
        CreateAsteroidEntities(amount);
    }

    public void Remove()
    {
        
    }
    
    public void UpdateSlider(float input)
    {
        var asteroidCount = 1;
        switch (input)
        {
            case 1:
                asteroidCount = 1;
                break;
            case 2:
                asteroidCount = 10;
                break;
            case 3:
                asteroidCount = 50;
                break;
            case 4:
                asteroidCount = 100;
                break;
            case 5:
                asteroidCount = 500;
                break;
            case 6:
                asteroidCount = 1000;
                break;
            case 7:
                asteroidCount = 3333;
                break;
            case 8:
                asteroidCount = 5000;
                break;
            case 9:
                asteroidCount = 10000;
                break;
            case 10:
                asteroidCount = 50000;
                break;
        }
        amount = asteroidCount;
    }

    void Start()
    {
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        convertedPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(asteroidPrefab, settings);

        CreateAsteroidEntities(initialNumberOfAsteroids);
    }

    private void CreateAsteroidEntities(int numberOfAsteroids)
    {
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            var instance = manager.Instantiate(convertedPrefab);
            float x = Mathf.Sin(i) * UnityEngine.Random.Range(70, 770);
            float y = UnityEngine.Random.Range(-20f, 20f);
            float z = Mathf.Cos(i) * UnityEngine.Random.Range(70, 770);
            float3 position = transform.TransformPoint(new float3(x, y, z));
            manager.SetComponentData(instance, new Translation {Value = position});

            var scaleDiff = 5f;

            var maxSize = math.distance(position, new Vector3(0, 0, 0)) / 3;

            var scaleBase = UnityEngine.Random.Range(1f, maxSize);
            var scalexdiff = UnityEngine.Random.Range(0, scaleDiff);
            var scaleydiff = UnityEngine.Random.Range(0, scaleDiff / 2);
            var scalezdiff = UnityEngine.Random.Range(0, scaleDiff);

            var scale = new Vector3(scaleBase + scalexdiff, scaleBase + scaleydiff, scaleBase + scalezdiff);

            manager.SetComponentData(instance, new NonUniformScale {Value = scale});

            var q = Quaternion.Euler(new Vector3(0, 0, 0));
            manager.SetComponentData(instance, new Rotation {Value = new quaternion(q.x, q.y, q.z, q.w)});

            float3 pivot = new float3(0, 0, 0);
            float3 _axisAngle = position - pivot;
            float tweakRange = 0.1f;
            float tweakValue = UnityEngine.Random.Range(1 - tweakRange, 1 + tweakRange);
            float dist = math.distance(position, pivot);
            var rotationalSpeed = math.sqrt((10 * 100) / dist);
            var initialVector = math.cross(new float3(0, 1, 0), math.normalize(position));

            manager.SetComponentData(instance, new AsteroidData
            {
                axisAngle = _axisAngle,
                velocity = initialVector * rotationalSpeed * tweakValue
            });
        }
    }
}