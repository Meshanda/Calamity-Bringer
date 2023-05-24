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
    [Unity.Burst.BurstCompile]
    public partial struct CountNumCollisionEvents : ICollisionEventsJob
    {
        public NativeReference<int> NumCollisionEvents;
        public EntityCommandBuffer SpeciesCollisionBuffer;

        public void Execute(CollisionEvent collisionEvent)
        {
            NumCollisionEvents.Value++;
            var entityA = collisionEvent.EntityA;
            var entityB = collisionEvent.EntityB;
            EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

            CapsuleTag capTag;
            BuildingTag buildTag;
            
            //Debug.Log($"A: {World.DefaultGameObjectInjectionWorld.EntityManager.GetName(entityA)}" +
            //           $", B: {World.DefaultGameObjectInjectionWorld.EntityManager.GetName(entityB)}");
            //ComponentLookup<CapsuleTag> look = new ComponentLookup<CapsuleTag>();
            //SystemAPI.GetComponent<CapsuleTag>(entityA);
            
            if ((entityManager.HasComponent<CapsuleTag>(entityA) && entityManager.HasComponent<BuildingTag>(entityB))
                || (entityManager.HasComponent<CapsuleTag>(entityB) && (entityManager.HasComponent<BuildingTag>(entityA)) ))
            {
                //if (entityManager.HasComponent<CapsuleTag>(entityA))
                //{
                //    capTag = entityManager.GetComponentData<CapsuleTag>(entityA);
                //}
                //else
                //{
                //    capTag = entityManager.GetComponentData<CapsuleTag>(entityB);
                //}

                if (entityManager.HasComponent<BuildingTag>(entityA))
                {
                    //entityManager.AddComponent<ExplodeComponent>(entityB);
                    //buildTag = entityManager.GetComponentData<BuildingTag>(entityA);
                    SpeciesCollisionBuffer.AddComponent<ExplodeComponent>( entityA);
                }
                else
                {
                    //entityManager.AddComponent<ExplodeComponent>(entityB);
                    //buildTag = entityManager.GetComponentData<BuildingTag>(entityB);
                    SpeciesCollisionBuffer.AddComponent<ExplodeComponent>(entityB);
                }
                Debug.Log($"A: {World.DefaultGameObjectInjectionWorld.EntityManager.GetName(entityA)}" +
                 $", B: {World.DefaultGameObjectInjectionWorld.EntityManager.GetName(entityB)} ");
            }

            //CapsuleTag capsule = 


        }

        //public  void SetEntities(Entity A, Entity B,out CapsuleTag capTag,out BuildingTag buildTag) 
        //{
            
        //}

        //public void Execute(TriggerEvent triggerEvent)
        //{
        //    NumCollisionEvents.Value++;
        //}
    }

    [Unity.Burst.BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        NativeReference<int> numCollisionEvents = new NativeReference<int>(0, Allocator.TempJob);
        var beginInitBufferSystem = SystemAPI.GetSingleton<BeginInitializationEntityCommandBufferSystem.Singleton>();
        //World.GetOrCreateSystemManaged<BeginInitializationEntityCommandBufferSystem>();

        var job = new CountNumCollisionEvents
        {
            NumCollisionEvents = numCollisionEvents,
            SpeciesCollisionBuffer = beginInitBufferSystem.CreateCommandBuffer(state.WorldUnmanaged)
        };

        job.Schedule<CountNumCollisionEvents>(
            SystemAPI.GetSingleton<SimulationSingleton>(),
             state.Dependency
            ).Complete();

        UnityEngine.Debug.Log("CollisionP: " + numCollisionEvents.Value);

        numCollisionEvents.Dispose();
    }
}
