using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;

[BurstCompile]
public partial struct PeopleSmashSystem : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float3 playerPosition = PlayerSingleton.Instance.transform.position;
        EntityCommandBuffer entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

        new SmashPeopleJob
        {
            EntityCommandBuffer = entityCommandBuffer,
            PlayerPosition = playerPosition 
        }.Schedule();
    }
}
