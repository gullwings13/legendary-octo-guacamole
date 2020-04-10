using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine.Rendering;

public class ECSManager : MonoBehaviour
{

    public GameObject asteroidPrefab;
    public const int initialNumberOfAsteroids = 1;
    public TextMeshProUGUI entityCount;
    // public int amount;
    
    private EntityManager manager;
    private EntityQuery entityQuery;
    private GameObjectConversionSettings settings;
    private Entity convertedPrefab;

    public float distanceBase = 20;
    public float minDistance = 50;
    public float maxDistance = 750;
    public float fieldHeight = 20;
    public float tweakRange = 0.1f;
    public float relativeSize = 1f;


    public TextMeshProUGUI minDistanceText;
    public TextMeshProUGUI maxDistanceText;
    public TextMeshProUGUI fieldHeightText;
    public TextMeshProUGUI tweakRangeText;
    
    public TextMeshProUGUI relativeSizeText;
    public TextMeshProUGUI asteroidsToAddText;
    public TextMeshProUGUI simSpeedText;
    public void AdjustMinDistance(float value)
    {
        // 1 to 1000
        value = Mathf.Round(value * 100f) / 100f;
        minDistance = value;
        minDistanceText.SetText(value.ToString());
    }
    
    public void AdjustMaxDistance(float value)
    {
        // 1 to 1000
        value = Mathf.Round(value * 100f) / 100f;
        maxDistance = value;
        maxDistanceText.SetText(value.ToString());
    }
    
    public void AdjustFieldHeight(float value)
    {
        // 0 to 100
        value = Mathf.Round(value * 100f) / 100f;
        fieldHeight = value;
        fieldHeightText.SetText(value.ToString());
    }
    
    public void AdjustTweakRange(float value)
    {
        // 0 to 10
        value = Mathf.Round(value * 100f) / 100f;
        tweakRange = value;
        tweakRangeText.SetText(value.ToString());
    }
    
    public void AdjustSize(float value)
    {
        // 0.1 to 10
        value = Mathf.Round(value * 100f) / 100f;
        relativeSize = value;
        relativeSizeText.SetText(value.ToString());
    }
    
    public void AdjustAsteroidCount(float value)
    {
        var asteroidCount = (int) math.round(math.pow(10, value));
        GameDataManager.instance.addRemoveAmount = asteroidCount;
        asteroidsToAddText.SetText(asteroidCount.ToString());
    }
    
    public void AdjustSimSpeed(float value)
    {
        // 0.1 to 3
        value = Mathf.Round(value * 100f) / 100f;
        GameDataManager.instance.timeScale = value;
        simSpeedText.SetText(value.ToString());
    }
    
    public void Add()
    {
        var additionalAsteroids = GameDataManager.instance.addRemoveAmount;
        GameDataManager.instance.adjustAsteroidCount(additionalAsteroids);
        CreateAsteroidEntities(additionalAsteroids);
    }

    public void Remove()
    {
        var subtractiveAsteroids = -GameDataManager.instance.addRemoveAmount;
        GameDataManager.instance.adjustAsteroidCount(subtractiveAsteroids);
    }

    void Start()
    {
        manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        convertedPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(asteroidPrefab, settings);
        entityQuery = manager.CreateEntityQuery(ComponentType.ReadOnly<AsteroidData>());
        CreateAsteroidEntities(initialNumberOfAsteroids);
    }
    
    private void Update()
    {
        entityCount.SetText(entityQuery.CalculateEntityCount().ToString());
    }

    private void CreateAsteroidEntities(int numberOfAsteroids)
    {
        
        for (int i = 0; i < numberOfAsteroids; i++)
        {
            var instance = manager.Instantiate(convertedPrefab);
            float x = Mathf.Sin(i) * UnityEngine.Random.Range(distanceBase+minDistance, distanceBase+maxDistance);
            float y = UnityEngine.Random.Range(-fieldHeight, fieldHeight);
            float z = Mathf.Cos(i) * UnityEngine.Random.Range(distanceBase+minDistance, distanceBase+maxDistance);
            float3 position = transform.TransformPoint(new float3(x, y, z));
            manager.SetComponentData(instance, new Translation {Value = position});

            var scaleDiff = 5f;

            var maxSize = math.distance(position, new Vector3(0, 0, 0)) / 3 * relativeSize;

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
            float tweakValue = UnityEngine.Random.Range(1 - tweakRange, 1 + tweakRange);
            float dist = math.distance(position, pivot);
            var rotationalSpeed = math.sqrt((10 * 100) / dist);
            var initialVector = math.cross(new float3(0, 1, 0), math.normalize(position));

            manager.SetComponentData(instance, new AsteroidData
            {
                axisAngle = _axisAngle,
                velocity = initialVector * rotationalSpeed * tweakValue * Time.fixedDeltaTime * 50
            });
        }
    }
}