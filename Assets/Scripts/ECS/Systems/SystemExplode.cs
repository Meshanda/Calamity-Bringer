using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;
using static UnityEngine.EventSystems.EventTrigger;

public partial struct SystemExplode : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();

        Entity expl ;
        if (!SystemAPI.TryGetSingletonEntity<ExplodeComponent>(out expl))
            return;
        var beginInitBufferSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        if (entityManager.HasComponent<BuildingTag>(expl)) 
        {
            var tag = entityManager.GetComponentData<BuildingTag>(expl);
            
            ColliderDestroyerSingleton.Instance.DestroyCollider(tag.index);
        }
        var buffer = entityManager.GetBuffer<LinkedEntityGroup>(expl).ToNativeArray(Unity.Collections.Allocator.FirstUserIndex);

        foreach (LinkedEntityGroup entity in buffer)
        {
            if (!entityManager.HasComponent<BuildingTag>(entity.Value))
            {
                entityManager.AddComponent<DebrisExplosion>(entity.Value);
                Debug.Log(entityManager.HasComponent<DebrisExplosion>(entity.Value));
            }
            //Debug.Log($"A: {World.DefaultGameObjectInjectionWorld.EntityManager.GetName(entity.Value)}");

        }
        new ExplodeJob
        {
            SpeciesCollisionBuffer = beginInitBufferSystem.CreateCommandBuffer(state.WorldUnmanaged)
        }.Schedule();
        state.Dependency.Complete();

        
    }
}

[BurstCompile]
public partial struct ExplodeJob : IJobEntity
{
    public EntityCommandBuffer SpeciesCollisionBuffer;
    [BurstCompile]
    private void Execute(Entity cap, [EntityIndexInQuery] int sortKey)
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        if (!entityManager.HasComponent<BuildingTag>(cap))
        {
            return;
        }

        
        //SpeciesCollisionBuffer.DestroyEntity(cap);
        SpeciesCollisionBuffer.RemoveComponent<PhysicsCollider>(cap);
        SpeciesCollisionBuffer.RemoveComponent<PhysicsMass>(cap);
        SpeciesCollisionBuffer.RemoveComponent<ExplodeComponent>(cap);
        
    }
}
