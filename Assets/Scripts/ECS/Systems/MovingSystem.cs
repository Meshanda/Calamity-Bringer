using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;
using Unity.Collections;

[BurstCompile]

[UpdateAfter(typeof(PersonSpawnerSystem))]
public partial struct MovingSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<RandomComponent>();
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        float deltaTime = SystemAPI.Time.DeltaTime;

        JobHandle jobHandle = new MoveJob
        {
            DeltaTime = deltaTime
        }.ScheduleParallel(state.Dependency);

        jobHandle.Complete();

        EntityQuery peopleEntityQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(PersonZone));
        NativeArray<PersonZone> movementZones = peopleEntityQuery.ToComponentDataArray<PersonZone>(Allocator.TempJob);

        new TestReachTaretPositionJob
        {
            RandomComponent = randomComponent,
            ZoneList = movementZones
        }.Run();

        movementZones.Dispose();
    }
}
