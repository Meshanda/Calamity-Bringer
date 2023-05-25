using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Collections;

[BurstCompile]
public partial struct TestReachTaretPositionJob : IJobEntity
{
    [NativeDisableUnsafePtrRestriction] public RefRW<RandomComponent> RandomComponent;
    public NativeArray<PersonZone> ZoneList; 

    public void Execute(PersonAspect personAspect)
    {
        personAspect.TestReachedTargetPosition(RandomComponent, ZoneList);
    }
}
