using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public partial struct SystemExplode : ISystem
{

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var ecbSingleton = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>();
        var expl = SystemAPI.GetSingletonEntity<ExplodeComponent>();
        var beginInitBufferSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        if (entityManager.HasComponent<BuildingTag>(expl)) 
        {
            var tag = entityManager.GetComponentData<BuildingTag>(expl);
            
            ColliderDestroyerSingleton.Instance.DestroyCollider(tag.index);
        }

        new ExplodeJob
        {
            SpeciesCollisionBuffer = beginInitBufferSystem.CreateCommandBuffer(state.WorldUnmanaged)
        }.Schedule();
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
        if (!entityManager.HasComponent<PhysicsCollider>(cap) || !entityManager.HasComponent<BuildingTag>(cap))
            return;

        var buffer = entityManager.GetBuffer<Child>(cap);
        foreach( var entity in buffer) 
        {
            Debug.Log($"A: {World.DefaultGameObjectInjectionWorld.EntityManager.GetName(entity.Value)}");
            
        }
        SpeciesCollisionBuffer.DestroyEntity(cap);
        //SpeciesCollisionBuffer.RemoveComponent<PhysicsCollider>(cap);
        Debug.Log("a");
    }
}
