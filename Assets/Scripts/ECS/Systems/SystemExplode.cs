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

        List<Entity> list = new List<Entity>(); 
        List<Entity> listEntity = new List<Entity>(); 
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var beginInitBufferSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

        foreach (var (_, entity) in SystemAPI.Query<ExplodeComponent>().WithEntityAccess())
        {
           
            if (entityManager.HasComponent<BuildingTag>(entity))
            {
                var tag = entityManager.GetComponentData<BuildingTag>(entity);
                ColliderDestroyerSingleton.Instance.DestroyCollider(tag.index);
            }
            var buffer = entityManager.GetBuffer<LinkedEntityGroup>(entity).ToNativeArray(Unity.Collections.Allocator.Temp);

            foreach (LinkedEntityGroup child in buffer)
            {
                if (!entityManager.HasComponent<BuildingTag>(child.Value))
                {
                    list.Add(child.Value);
                }
                else
                    listEntity.Add(child.Value);

            }
            new ExplodeJob
            {
                cap = entity,
                SpeciesCollisionBuffer = beginInitBufferSystem.CreateCommandBuffer(state.WorldUnmanaged)
            }.Schedule(state.Dependency).Complete();
        }

        foreach (var child in list)
        {
            entityManager.AddComponent<DebrisExplosion>(child);
        }



    }
}

[BurstCompile]
public partial struct ExplodeJob : IJobEntity
{
    public EntityCommandBuffer SpeciesCollisionBuffer;
    public Entity cap;
    [BurstCompile]
    private void Execute( [EntityIndexInQuery] int sortKey)
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        if (!entityManager.HasComponent<BuildingTag>(cap))
        {
            return;
        }

        
        //SpeciesCollisionBuffer.DestroyEntity(cap);
        SpeciesCollisionBuffer.RemoveComponent<PhysicsCollider>(cap);
        SpeciesCollisionBuffer.RemoveComponent<PhysicsMass>(cap);
        entityManager.SetComponentEnabled<ExplodeComponent>(cap, false);

    }
}
