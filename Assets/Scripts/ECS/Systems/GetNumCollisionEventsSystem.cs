using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateBefore(typeof(PhysicsSimulationGroup))] // We are updating before `PhysicsSimulationGroup` - this means that we will get the events of the previous frame
public partial struct GetNumCollisionEventsSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SimulationSingleton>();
    }

    [Unity.Burst.BurstCompile]
    public partial struct CountNumCollisionEvents : ICollisionEventsJob
    {
        public void Execute(CollisionEvent collisionEvent)
        {
            var entityA = collisionEvent.EntityA;
            var entityB = collisionEvent.EntityB;
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;



            if ((entityManager.HasComponent<CapsuleTag>(entityA) && entityManager.HasComponent<BuildingTag>(entityB))
                || (entityManager.HasComponent<CapsuleTag>(entityB) && (entityManager.HasComponent<BuildingTag>(entityA)) ))
            {
                if (entityManager.HasComponent<BuildingTag>(entityA))
                {
                    entityManager.SetComponentEnabled<ExplodeComponent>(entityA, true);
                }
                else
                {
                    entityManager.SetComponentEnabled<ExplodeComponent>(entityB, true);
                }
            }


        }

    }

    [Unity.Burst.BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var job = new CountNumCollisionEvents();

        job.Schedule<CountNumCollisionEvents>(
            SystemAPI.GetSingleton<SimulationSingleton>(),
             state.Dependency
            ).Complete();


        //UnityEngine.Debug.Log("CollisionP: " + numCollisionEvents.Value);

    }
}
