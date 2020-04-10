using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class MoveSystem : JobComponentSystem
{
    [BurstCompile]
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.fixedDeltaTime * GameDataManager.instance.timeScale * 50;
        float3 targetLocation = new float3(0, 0, 0);

        var jobHandle = Entities.WithName("MoveSystem").ForEach(
            (ref Translation position, ref Rotation rotation, ref AsteroidData asteroidData) =>
            {
                float3 diff = position.Value - targetLocation;

                float dist = math.distance(position.Value, targetLocation);
                float3 direction = math.normalize(diff);
                float3 movementVector = asteroidData.velocity + (-direction * (10 * (1 * 100 / (dist * dist)))*deltaTime);
                asteroidData.velocity = movementVector;

                position.Value += asteroidData.velocity * deltaTime;
            }).Schedule(inputDeps);

        return jobHandle;
    }
}