using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Jobs;

// public class DestroySystem : JobComponentSystem {
//  
//     [BurstCompile]
//     struct DestroyJob : IJobForEach<AsteroidData>
//     {
//         public float dt;
//  
//         public void Execute(ref BulletLifeTime bulletLifeTime)
//         {
//             if(bulletLifeTime.Value < 0f)
//             {
//                 //destroy bullet
//             }
//             else
//                 bulletLifeTime.Value -= dt;
//         }
//
//         public void Execute(ref AsteroidData c0)
//         {
//             throw new System.NotImplementedException();
//         }
//     }
//  
//     protected override JobHandle OnUpdate(JobHandle inputDeps)
//     {
//         inputDeps = new DestroyJob()
//         {
//             dt = Time.deltaTime
//         }.Schedule(this, inputDeps);
//         return inputDeps;
//     }
//    
// }

public class DestroySystem : JobComponentSystem {
    private EndSimulationEntityCommandBufferSystem endSimCommandBufferSystem;
 
    [BurstCompile]
    struct DestroyJob : IJobForEachWithEntity<AsteroidData>
    {
        public float limit;
        public EntityCommandBuffer.Concurrent CommandBuffer;

        public void Execute(Entity entity, int index, ref AsteroidData c0)
        {
            if(index-1 > limit)
            {
                //destroy asteroid
                CommandBuffer.DestroyEntity(index, entity);
            }
        }
    }
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new DestroyJob()
        {
            limit = GameDataManager.instance.AsteroidCount,
            CommandBuffer = endSimCommandBufferSystem.CreateCommandBuffer().ToConcurrent()
        };
 
        var jobHandle = job.Schedule(this, inputDeps);
        endSimCommandBufferSystem.AddJobHandleForProducer(jobHandle);
        return jobHandle;
    }
 
    protected override void OnCreate()
    {
        endSimCommandBufferSystem = World.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
        base.OnCreate();
    }
}