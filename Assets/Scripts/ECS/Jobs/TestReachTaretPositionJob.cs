using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Burst;

[BurstCompile]
public partial struct TestReachTaretPositionJob : IJobEntity
{
    [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> RandomComponent;
    
    public void Execute(MoveToPositionAspect moveToPositionAspect)
    {
        moveToPositionAspect.TestReachedTargetPosition(RandomComponent);
    }
}
