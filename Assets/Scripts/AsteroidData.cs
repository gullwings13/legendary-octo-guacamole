using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct AsteroidData : IComponentData
{
   public float3 axisAngle;
   public float3 velocity;
}
