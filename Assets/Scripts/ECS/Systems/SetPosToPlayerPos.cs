using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;
using static UnityEditor.PlayerSettings;

[BurstCompile]
public partial struct SetPosToPlayerPosSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<CapsuleTag>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var deltaTime = SystemAPI.Time.DeltaTime;
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var capsuleEntity = SystemAPI.GetSingletonEntity<CapsuleTag>();
        var posTemp = GetCapsuleEntity.Instance.transform.position;
        var quatTemp = GetCapsuleEntity.Instance.transform.rotation;

        //RefRW<Transform> transform =;
         new ZombieWalkJob
        {
             pos = posTemp,
             quat = quatTemp
         }.Schedule();

        //World.GetOrCreateSystem<StepPhysicsWorld>().FinalJobHandle.Complete();
    }
}

[BurstCompile]
public partial struct ZombieWalkJob : IJobEntity
{
    public float3 pos;
    public quaternion quat;
    [BurstCompile]
    private void Execute(CapsuleAspect cap, [EntityIndexInQuery] int sortKey)
    {
        cap.SetPos(pos, quaternion.identity);
    }
}
