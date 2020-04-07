using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class MoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        float deltaTime = Time.DeltaTime;
        float speed = 750f;
        
        float3 targetLocation = new float3(0,0,0);
        
        var jobHandle = Entities.
            WithName("MoveSystem").
            ForEach((ref Translation position, ref Rotation rotation, ref AsteroidData asteroidData) =>
            {
                // float3 pivot = targetLocation;

                // var rotationalSpeed = deltaTime * speed * 1 / (math.distance(position.Value, pivot)*6f);

                

                // position.Value = math.mul(quaternion.AxisAngle(
                //     new float3(0, 1, 0),
                //     (asteroidData.axisAngle / asteroidData.axisAngle),
                //     rotationalSpeed), position.Value - pivot) + pivot;

                    float3 diff = position.Value - targetLocation;

                    float dist = math.distance(position.Value, targetLocation);
                    float3 direction = math.normalize(diff);
                    float3 movementVector = asteroidData.velocity +  -direction * ( 10 * (1 * 100 / (dist * dist)));
                    asteroidData.velocity = movementVector;

                    position.Value += asteroidData.velocity;

            }).Schedule(inputDeps);
        
        return jobHandle;
    }
}
