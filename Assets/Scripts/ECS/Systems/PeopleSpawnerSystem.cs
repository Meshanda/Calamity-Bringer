using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class PeopleSpawnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        EntityQuery peopleEntityQuery = EntityManager.CreateEntityQuery(typeof(PeopleTag));
        PeopleSpawnerComponent peopleSpawnerComponent = SystemAPI.GetSingleton<PeopleSpawnerComponent>();
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

        EntityCommandBuffer entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

        if (peopleEntityQuery.CalculateEntityCount() < peopleSpawnerComponent.MaxNbPeople)
        {
            Entity spawnedEntity = entityCommandBuffer.Instantiate(peopleSpawnerComponent.PlayerPrefab);

            float3 startPos = new float3(
                randomComponent.ValueRW.Random.NextFloat(peopleSpawnerComponent.SpawnCenterZone.x - (peopleSpawnerComponent.SizeXZone / 2),
                                                         peopleSpawnerComponent.SpawnCenterZone.x + (peopleSpawnerComponent.SizeXZone / 2)),
                0,
                randomComponent.ValueRW.Random.NextFloat(peopleSpawnerComponent.SpawnCenterZone.z - (peopleSpawnerComponent.SizeZZone / 2),
                                                         peopleSpawnerComponent.SpawnCenterZone.z + (peopleSpawnerComponent.SizeZZone / 2))
            );

            // Set Start position
            entityCommandBuffer.SetComponent(spawnedEntity, new LocalTransform
            {
                Position = startPos,
                Rotation = quaternion.identity,
                Scale = 1
            });

            // Change the speed of the person
            entityCommandBuffer.SetComponent(spawnedEntity, new Speed
            {
                Value = randomComponent.ValueRW.Random.NextFloat(peopleSpawnerComponent.PeopleMinSpeed, peopleSpawnerComponent.PeopleMaxSpeed)
            });

            
            // Change destination
            entityCommandBuffer.SetComponent(spawnedEntity, new TargetPosition
            {
                Value = new float3(
                                    randomComponent.ValueRW.Random.NextFloat(-500f, 500f),
                                    0,
                                    randomComponent.ValueRW.Random.NextFloat(-200f, 200f))
            });
            
        }
    }
}
