using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct PersonMovementZone : IComponentData
{
    public float3 SpawnCenterZone;

    public float SizeXZone;
    public float SizeZZone;
}
