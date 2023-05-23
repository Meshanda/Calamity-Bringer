using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[UpdateBefore(typeof(PhysicsSimulationGroup))] // We are updating before `PhysicsSimulationGroup` - this means that we will get the events of the previous frame
public partial struct GetNumCollisionEventsSystem : ISystem
{
    [Unity.Burst.BurstCompile]
    public partial struct CountNumCollisionEvents : ICollisionEventsJob
    {
        public NativeReference<int> NumCollisionEvents;
        public void Execute(CollisionEvent collisionEvent)
        {
            NumCollisionEvents.Value++;
        }

        //public void Execute(TriggerEvent triggerEvent)
        //{
        //    NumCollisionEvents.Value++;
        //}
    }

    [Unity.Burst.BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        NativeReference<int> numCollisionEvents = new NativeReference<int>(0, Allocator.TempJob);

        var job = new CountNumCollisionEvents
        {
            NumCollisionEvents = numCollisionEvents
        };

        job.Schedule<CountNumCollisionEvents>(
            SystemAPI.GetSingleton<SimulationSingleton>(),
             state.Dependency
            ).Complete();

        //UnityEngine.Debug.Log("CollisionP: " + numCollisionEvents.Value);

        numCollisionEvents.Dispose();
    }
}
