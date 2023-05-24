using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class PeopleSpawnerSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<PeopleSpawnerComponent>();
        RequireForUpdate<RandomComponent>();
    }
    
    protected override void OnUpdate()
    {
        EntityQuery peopleEntityQuery = EntityManager.CreateEntityQuery(typeof(PeopleTag));
        PeopleSpawnerComponent peopleSpawnerComponent = SystemAPI.GetSingleton<PeopleSpawnerComponent>();

        if (peopleEntityQuery.CalculateEntityCount() < peopleSpawnerComponent.MaxNbPeople)
        {
            RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
            EntityCommandBuffer entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

            Entity spawnedEntity = entityCommandBuffer.Instantiate(peopleSpawnerComponent.PlayerPrefab);

            float3 startPosition = GetSpawnPosition(randomComponent);

            // Set Start position
            entityCommandBuffer.SetComponent(spawnedEntity, new LocalTransform
            {
                Position = startPosition,
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
                Value = startPosition + new float3(0.1f, 0, 0.1f)
            });          
        }
    }

    private float3 GetSpawnPosition(RefRW<RandomComponent> randomComponent)
    {
        List<PersonSpawnZone> spawnZones = new List<PersonSpawnZone>();
        foreach (var item in SystemAPI.Query<PersonSpawnZone>())
        {
            spawnZones.Add(item);
        }

        int randomZone = randomComponent.ValueRW.Random.NextInt(0, spawnZones.Count);
        PersonSpawnZone spawnZone = spawnZones[randomZone];

        float startPosX = randomComponent.ValueRW.Random.NextFloat(spawnZone.SpawnCenterZone.x - (spawnZone.SizeXZone / 2), spawnZone.SpawnCenterZone.x + (spawnZone.SizeXZone / 2));
        float startPosZ = randomComponent.ValueRW.Random.NextFloat(spawnZone.SpawnCenterZone.z - (spawnZone.SizeZZone / 2), spawnZone.SpawnCenterZone.z + (spawnZone.SizeZZone / 2));

        return new float3(startPosX, 0, startPosZ);
    }
}
