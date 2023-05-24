using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Burst;
using Unity.Mathematics;

[BurstCompile]
public partial struct TestReachTaretPositionJob : IJobEntity
{
    public float3 Destination;
    
    public void Execute(MoveToPositionAspect moveToPositionAspect)
    {
        moveToPositionAspect.TestReachedTargetPosition(Destination);
    }
}
