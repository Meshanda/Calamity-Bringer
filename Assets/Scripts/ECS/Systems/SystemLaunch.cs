using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public partial struct SystemLaunch : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var beginInitBufferSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        //DynamicBuffer<Child> buffer = entityManager.GetBuffer<Child>(expl);
        //Debug.Log(childFromEntity.Length);
        
        float3 originExpl = GetCapsuleEntity.Instance.transform.position;   
        new Launch
        {
            pos = originExpl
        }.ScheduleParallel();
    }
}
[BurstCompile]
public partial struct Launch : IJobEntity
{
    public float3 pos;
    public quaternion quat;
    [BurstCompile]
    private void Execute(DebrisAscpect cap, [EntityIndexInQuery] int sortKey)
    {
        cap.Launch(pos);
    }
}