using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Jobs;
using Unity.Burst;
using Unity.Mathematics;

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
            Destination = GetDestinationPosition(state, randomComponent)
        }.Run();
    }

    private float3 GetDestinationPosition(SystemState state, RefRW<RandomComponent> randomComponent)
    {
        List<PersonMovementZone> movementZones = new List<PersonMovementZone>();
        foreach (var item in SystemAPI.Query<PersonMovementZone>())
        {
            movementZones.Add(item);
        }

        int randomZone = randomComponent.ValueRW.Random.NextInt(0, movementZones.Count);
        PersonMovementZone movementZone = movementZones[randomZone];

        float startPosX = randomComponent.ValueRW.Random.NextFloat(movementZone.SpawnCenterZone.x - (movementZone.SizeXZone / 2), movementZone.SpawnCenterZone.x + (movementZone.SizeXZone / 2));
        float startPosZ = randomComponent.ValueRW.Random.NextFloat(movementZone.SpawnCenterZone.z - (movementZone.SizeZZone / 2), movementZone.SpawnCenterZone.z + (movementZone.SizeZZone / 2));

        return new float3(startPosX, 0, startPosZ);

    }
}
