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
        var beginInitBufferSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
        var commandBuffer = beginInitBufferSystem.CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();

        new ApplyGravity
        {
            SpeciesCollisionBuffer = commandBuffer,
            DeltaTime = deltatTime
        }.ScheduleParallel();
    }
}
[BurstCompile]
public partial struct ApplyGravity : IJobEntity
{
    public float DeltaTime;
     public EntityCommandBuffer.ParallelWriter SpeciesCollisionBuffer;
    [BurstCompile]
    private void Execute(DebrisAscpect cap, [EntityIndexInQuery] int sortKey)
    {
        bool toDestroy = cap.AddGravity(DeltaTime);
        if(!toDestroy) 
        {
            SpeciesCollisionBuffer.DestroyEntity(sortKey,cap.Entity);
        }
    }
}