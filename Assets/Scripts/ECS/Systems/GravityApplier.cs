using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public partial struct  GravityApplier : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltatTime = SystemAPI.Time.DeltaTime;
        new ApplyGravity
        {
            DeltaTime = deltatTime
        }.Schedule();
    }
}
[BurstCompile]
public partial struct ApplyGravity : IJobEntity
{
    public float DeltaTime;
    [BurstCompile]
    private void Execute(DebrisAscpect cap, [EntityIndexInQuery] int sortKey)
    {
        cap.AddGravity(DeltaTime);
    }
}