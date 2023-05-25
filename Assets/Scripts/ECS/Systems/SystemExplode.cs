using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Physics;

public partial struct SystemExplode : ISystem
{
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        List<Entity> list = new List<Entity>(); 
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var beginInitBufferSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();

        foreach (var (buildingAspect, entity) in SystemAPI.Query<BuildingAspect>().WithEntityAccess())
        {
            
            buildingAspect.DestroyCollider();

            var commandBuffer = beginInitBufferSystem.CreateCommandBuffer(state.WorldUnmanaged);
            
            commandBuffer.RemoveComponent<BuildingTag>(entity);
            
            var buffer = entityManager.GetBuffer<LinkedEntityGroup>(entity).ToNativeArray(Unity.Collections.Allocator.Temp);

            foreach (LinkedEntityGroup child in buffer)
            {
                if (!entityManager.HasComponent<BuildingTag>(child.Value))
                {
                    list.Add(child.Value);
                }

            }
            new ExplodeJob
            {
                SpeciesCollisionBuffer = commandBuffer
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
    
    [BurstCompile]
    private void Execute(BuildingAspect building)
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        
        //SpeciesCollisionBuffer.DestroyEntity(cap);
        SpeciesCollisionBuffer.RemoveComponent<PhysicsCollider>(building.Entity);
        SpeciesCollisionBuffer.RemoveComponent<PhysicsMass>(building.Entity);
        entityManager.SetComponentEnabled<ExplodeComponent>(building.Entity, false);

    }
}
