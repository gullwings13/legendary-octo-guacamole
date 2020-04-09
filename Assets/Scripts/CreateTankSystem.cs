using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;

public class CreateTankSystem : JobComponentSystem
{
    protected override void OnCreate()
    {
        base.OnCreate();
        //
        // for (int i = 0; i < 10; i++)
        // {
        //     
        //     var instance = EntityManager.CreateEntity(
        //         ComponentType.ReadOnly<LocalToWorld>(),
        //         ComponentType.ReadWrite<Translation>(),
        //         ComponentType.ReadWrite<Rotation>(),
        //         ComponentType.ReadOnly<TankData>(),
        //         ComponentType.ReadOnly<NonUniformScale>()
        //         );
        //
        //     EntityManager.SetComponentData(instance, new NonUniformScale{Value = new float3(1,1,1)});
        //     // EntityManager.SetComponentData(instance, new LocalToWorld
        //     // {
        //     //     Value = new float4x4(rotation: quaternion.identity, translation: new float3(UnityEngine.Random.Range(-10,10),1,UnityEngine.Random.Range(-10,10)))
        //     // });
        //
        //     EntityManager.SetComponentData(instance,
        //         new Translation
        //             {Value = new float3(UnityEngine.Random.Range(-10, 10), 1, UnityEngine.Random.Range(-10, 10))}); 
        //     EntityManager.SetComponentData(instance, new Rotation{Value = new quaternion(0,0,0,0)});
        //
        //     var rHolder = Resources.Load<GameObject>("ResourceHolder").GetComponent<ResourceHolder>();
        //     
        //     // EntityManager.SetSharedComponentData(instance, new RenderMesh
        //     // {
        //     //     mesh = rHolder.tankChassisMesh,
        //     //     material = rHolder.tankGreenMaterial
        //     // });
        //     
        //     var chassisGreenInstance = EntityManager.CreateEntity(
        //         ComponentType.ReadOnly<LocalToWorld>(),
        //         ComponentType.ReadOnly<LocalToParent>(),
        //         ComponentType.ReadOnly<Parent>(),
        //         ComponentType.ReadOnly<RenderMesh>()
        //     );
        //     EntityManager.SetComponentData(chassisGreenInstance, new Parent{Value = instance});
        //     EntityManager.SetComponentData(chassisGreenInstance, new LocalToParent
        //     {
        //         Value = new float4x4(rotation: quaternion.identity, translation: new float3(0,0,0))
        //     });
        //     EntityManager.SetSharedComponentData(chassisGreenInstance, new RenderMesh
        //     {
        //         mesh = rHolder.tankChassisMesh,
        //         material = rHolder.tankGreenMaterial,
        //         subMesh = 0
        //     });
        //     
        //     
        //     // var chassisLightsInstance = EntityManager.CreateEntity(
        //     //     ComponentType.ReadOnly<LocalToWorld>(),
        //     //     ComponentType.ReadOnly<LocalToParent>(),
        //     //     ComponentType.ReadOnly<Parent>(),
        //     //     ComponentType.ReadOnly<RenderMesh>()
        //     // );
        //     // EntityManager.SetComponentData(chassisLightsInstance, new Parent{Value = instance});
        //     // EntityManager.SetComponentData(chassisLightsInstance, new LocalToParent
        //     // {
        //     //     Value = new float4x4(rotation: quaternion.identity, translation: new float3(0,0,0))
        //     // });
        //     // EntityManager.SetSharedComponentData(chassisLightsInstance, new RenderMesh
        //     // {
        //     //     mesh = rHolder.tankChassisMesh,
        //     //     material = rHolder.tankLightMaterial,
        //     //     subMesh = 1
        //     // });
        //     //
        //     // var chassisGreyInstance = EntityManager.CreateEntity(
        //     //     ComponentType.ReadOnly<LocalToWorld>(),
        //     //     ComponentType.ReadOnly<LocalToParent>(),
        //     //     ComponentType.ReadOnly<Parent>(),
        //     //     ComponentType.ReadOnly<RenderMesh>()
        //     // );
        //     // EntityManager.SetComponentData(chassisGreyInstance, new Parent{Value = instance});
        //     // EntityManager.SetComponentData(chassisGreyInstance, new LocalToParent
        //     // {
        //     //     Value = new float4x4(rotation: quaternion.identity, translation: new float3(0,0,0))
        //     // });
        //     // EntityManager.SetSharedComponentData(chassisGreyInstance, new RenderMesh
        //     // {
        //     //     mesh = rHolder.tankChassisMesh,
        //     //     material = rHolder.tankGreyMaterial,
        //     //     subMesh = 2
        //     // });
        //     //
        //     //
        //     //
        //     // var leftInstance = EntityManager.CreateEntity(
        //     //     ComponentType.ReadOnly<LocalToWorld>(),
        //     //     ComponentType.ReadOnly<LocalToParent>(),
        //     //     ComponentType.ReadOnly<Parent>(),
        //     //     ComponentType.ReadOnly<RenderMesh>()
        //     //     
        //     //     );
        //     //
        //     // EntityManager.SetComponentData(leftInstance, new Parent{Value = instance});
        //     //
        //     // EntityManager.SetComponentData(leftInstance, new LocalToParent
        //     // {
        //     //     Value = new float4x4(rotation: quaternion.identity, translation: new float3(0,0,0))
        //     // });
        //     // EntityManager.SetSharedComponentData(leftInstance, new RenderMesh
        //     // {
        //     //     mesh = rHolder.tankLeftMesh,
        //     //     material = rHolder.tankGreenMaterial
        //     // });
        // }
        //
    }


    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return inputDeps;
    }
}