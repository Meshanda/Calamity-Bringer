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
        if (GetCapsuleEntity.Instance is null) return;
        
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
    
    [BurstCompile]
    private void Execute(DebrisAscpect cap)
    {
        cap.Launch(pos);
    }
}