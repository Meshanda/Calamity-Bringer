using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;
using UnityEngine;

public partial struct SystemExplode : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        List<Entity> list = new List<Entity>(); 
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var ECB = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);

        foreach (var (buildingAspect, entity) in SystemAPI.Query<BuildingAspect>().WithEntityAccess())
        {
            
            buildingAspect.DestroyCollider();

            var buffer = entityManager.GetBuffer<LinkedEntityGroup>(entity).ToNativeArray(Unity.Collections.Allocator.Temp);

            foreach (LinkedEntityGroup child in buffer)
            {
                if (!entityManager.HasComponent<BuildingTag>(child.Value))
                {
                    list.Add(child.Value);
                }
            }
            
            ECB.RemoveComponent<PhysicsCollider>(entity);
            ECB.RemoveComponent<PhysicsMass>(entity);
            ECB.RemoveComponent<ExplodeComponent>(entity);
            ECB.RemoveComponent<BuildingTag>(entity);
            
        }

        foreach (var child in list)
        {
            entityManager.AddComponent<DebrisExplosion>(child);
        }
        
    }
}