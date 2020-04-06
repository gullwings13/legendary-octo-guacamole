using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class CreateSheepSystem : JobComponentSystem
{
    protected override void OnCreate()
    {
        base.OnCreate();


        // for (int i = 0; i < 10; i++)
        // {
        //     
        //     var instance = EntityManager.CreateEntity(
        //         ComponentType.ReadOnly<LocalToWorld>(),
        //         ComponentType.ReadWrite<Translation>(),
        //         ComponentType.ReadWrite<Rotation>(),
        //         ComponentType.ReadOnly<RenderMesh>(),
        //         ComponentType.ReadWrite<NonUniformScale>());
        //     
        //     EntityManager.SetComponentData(instance, new LocalToWorld
        //     {
        //         Value = new float4x4(rotation: quaternion.identity, translation: new float3(UnityEngine.Random.Range(-10,10),1,UnityEngine.Random.Range(-10,10)))
        //     });
        //
        //     var newScale = UnityEngine.Random.Range(30, 50);
        //     EntityManager.SetComponentData(instance, new NonUniformScale {Value = new float3(newScale,newScale,newScale)});
        //     EntityManager.SetComponentData(instance,
        //         new Translation
        //             {Value = new float3(UnityEngine.Random.Range(-10, 10), 1, UnityEngine.Random.Range(-10, 10))}); 
        //     EntityManager.SetComponentData(instance, new Rotation{Value = new quaternion(0,0,0,0)});
        //
        //     var rHolder = Resources.Load<GameObject>("ResourceHolder").GetComponent<ResourceHolder>();
        //     
        //     EntityManager.SetSharedComponentData(instance, new RenderMesh
        //     {
        //         mesh = rHolder.sheepMesh,
        //         material = rHolder.sheepMaterial
        //     });
        // }
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return inputDeps;
    }
}