using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

[BurstCompile]
public partial struct SystemExplode : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<EndSimulationEntityCommandBufferSystem.Singleton>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var ECB = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (buildingAspect, entity) in SystemAPI.Query<BuildingAspect>().WithEntityAccess())
        {
            
            buildingAspect.DestroyCollider();

            var buffer = entityManager.GetBuffer<LinkedEntityGroup>(entity).ToNativeArray(Unity.Collections.Allocator.Temp);

            foreach (LinkedEntityGroup child in buffer)
            {
                if (!entityManager.HasComponent<BuildingTag>(child.Value))
                {
                    ECB.AddComponent<DebrisExplosion>(child.Value);
                }
            }
            
            ECB.RemoveComponent<PhysicsCollider>(entity);
            ECB.RemoveComponent<PhysicsMass>(entity);
            ECB.RemoveComponent<ExplodeComponent>(entity);
            ECB.RemoveComponent<BuildingTag>(entity);
            
        }

        
    }
}