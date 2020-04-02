using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

public class CubeMoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var jobHandle = Entities.WithName("CubeMoveSystem").ForEach((ref Translation position, ref Rotation rotation, ref CubeData cubeData) =>
        {
            position.Value -= 0.1f * math.up();
            if (position.Value.y <0)
            {
                position.Value.y = 80;
            }
        }).Schedule(inputDeps);
        
        return jobHandle;
    }
}
