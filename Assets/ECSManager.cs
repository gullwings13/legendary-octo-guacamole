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
    const int numAsteroids = 110000;

    // Start is called before the first frame update
    void Start()
    {
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        var prefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(asteroidPrefab, settings);

        for (int i = 0; i < numAsteroids; i++)
        {
            var instance = manager.Instantiate(prefab);
            float x = Mathf.Sin(i) * UnityEngine.Random.Range(7, 770);
            float y = UnityEngine.Random.Range(-20f, 20f);
            float z = Mathf.Cos(i) * UnityEngine.Random.Range(7, 770);
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
            float tweakValue = 1.00f;
            // float initialSpeed = 450f;
            float dist = math.distance(position, pivot);

            // var rotationalSpeed =   (initialSpeed / (dist));
            
            
            var rotationalSpeed =   math.sqrt((10 * 100) / dist);
            
            
            // Debug.Log(rotationalSpeed);

            var initialVector = math.mul(quaternion.AxisAngle(
                new float3(0, 1, 0),
                // (_axisAngle / _axisAngle), 
                // 0.000001f
                1f
            ), position - pivot);
            // + position;


            var initalVectorNormed = math.normalize(initialVector);
            // Debug.Log(initalVectorNormed);
            manager.SetComponentData(instance, new AsteroidData
            {
                axisAngle = _axisAngle,
                // velocity = new float3(UnityEngine.Random.Range(-initialSpeed, initialSpeed),0,UnityEngine.Random.Range(-initialSpeed, initialSpeed) )});
                // velocity = initialVector
                // velocity = float3.zero
                velocity = initalVectorNormed * rotationalSpeed * tweakValue
            });
        }
    }
}