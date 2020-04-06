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
    const int numAsteroids = 105000;

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
            float y = UnityEngine.Random.Range(-10f, 10f);
            float z = Mathf.Cos(i) * UnityEngine.Random.Range(7, 770);
            float3 position = transform.TransformPoint(new float3(x, y, z));
            manager.SetComponentData(instance, new Translation { Value = position });

            var scaleDiff = 20f;

            var maxSize = math.distance(position, new Vector3(0, 0, 0));
            
            var scaleBase = UnityEngine.Random.Range(1f, maxSize);
            var scalexdiff = UnityEngine.Random.Range(0, scaleDiff);
            var scaleydiff = UnityEngine.Random.Range(0, scaleDiff/2);
            var scalezdiff = UnityEngine.Random.Range(0, scaleDiff);
            
            var scale = new Vector3(scaleBase+scalexdiff, scaleBase+scaleydiff, scaleBase+scalezdiff);
            
            manager.SetComponentData(instance, new NonUniformScale{Value = scale});

            var q = Quaternion.Euler(new Vector3(0, 0, 0));
            manager.SetComponentData(instance, new Rotation { Value = new quaternion(q.x,q.y,q.z,q.w) });
            
            float3 pivot = new float3(0,0,0);
            float3 _axisAngle = position - pivot;
            manager.SetComponentData(instance, new AsteroidData{axisAngle = _axisAngle, 
                velocity = new float3(UnityEngine.Random.Range(-100, 100),UnityEngine.Random.Range(-100, 100),UnityEngine.Random.Range(-100, 100) )});
        }

    }
}
