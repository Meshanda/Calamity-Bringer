using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class PersonSpawnerSystem : SystemBase
{
    protected override void OnCreate()
    {
        RequireForUpdate<PersonSpawnerComponent>();
        RequireForUpdate<RandomComponent>();
    }
    
    protected override void OnUpdate()
    {
        EntityQuery peopleEntityQuery = EntityManager.CreateEntityQuery(typeof(PeopleTag));
        PersonSpawnerComponent peopleSpawnerComponent = SystemAPI.GetSingleton<PersonSpawnerComponent>();

        if (peopleEntityQuery.CalculateEntityCount() < peopleSpawnerComponent.MaxNbPeople)
        {
            RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
            EntityCommandBuffer entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

            Entity spawnedEntity = entityCommandBuffer.Instantiate(peopleSpawnerComponent.PlayerPrefab);

            EntityQuery query = EntityManager.CreateEntityQuery(typeof(PersonZone));
            NativeArray<PersonZone> spawnZones = query.ToComponentDataArray<PersonZone>(Allocator.Persistent);

            int randomZoneIndex = randomComponent.ValueRW.Random.NextInt(0, spawnZones.Length);
            PersonZone zone = spawnZones[randomZoneIndex];

            float3 startPosition = GetSpawnPosition(zone, randomComponent);

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

            // Set the zone in which it should stay
            entityCommandBuffer.SetComponent(spawnedEntity, new MovementZoneIndex
            {
                MovementIndex = randomZoneIndex
            });                         
        }
    }

    private float3 GetSpawnPosition(PersonZone zone, RefRW<RandomComponent> randomComponent)
    {
        float startPosX = randomComponent.ValueRW.Random.NextFloat(zone.SpawnCenterZone.x - (zone.SizeXZone / 2), zone.SpawnCenterZone.x + (zone.SizeXZone / 2));
        float startPosZ = randomComponent.ValueRW.Random.NextFloat(zone.SpawnCenterZone.z - (zone.SizeZZone / 2), zone.SpawnCenterZone.z + (zone.SizeZZone / 2));

        return new float3(startPosX, 0, startPosZ);
    }
}
