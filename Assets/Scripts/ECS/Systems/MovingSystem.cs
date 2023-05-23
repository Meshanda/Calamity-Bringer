using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;

[BurstCompile]
public partial struct MovingSystem : ISystem
{
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

        new TestReachTaretPositionJob
        {
            RandomComponent = randomComponent
        }.Run();
    }
}
