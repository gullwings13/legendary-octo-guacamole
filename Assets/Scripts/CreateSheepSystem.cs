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

        var instance = EntityManager.CreateEntity(
            ComponentType.ReadOnly<LocalToWorld>(),
            ComponentType.ReadOnly<RenderMesh>());
        
        EntityManager.SetComponentData(instance, new LocalToWorld
        {
            Value = new float4x4(rotation: quaternion.identity, translation: new float3(1,1,1))
        });
        
        var rHolder = Resources.Load<GameObject>("ResourceHolder").GetComponent<ResourceHolder>();
        
        EntityManager.SetSharedComponentData(instance, new RenderMesh
        {
            mesh = rHolder.sheepMesh,
            material = rHolder.sheepMaterial
        });
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return inputDeps;
    }
}