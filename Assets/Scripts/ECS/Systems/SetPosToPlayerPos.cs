using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

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
        var posTemp = GetCapsuleEntity.Instance.transform.position;
        var quatTemp = GetCapsuleEntity.Instance.transform.rotation;

        //Collider col = GetCapsuleEntity.Instance.gameObject.GetComponent<Collider>();
        bool activated = GetCapsuleEntity.Instance.gameObject.active;
        //RefRW<Transform> transform =;
         new ZombieWalkJob
        {
             activated = activated,
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
    public bool activated;
    [BurstCompile]
    private void Execute(CapsuleAspect cap)
    {
        cap.SetPos(pos, quaternion.identity, activated);
    }
}
