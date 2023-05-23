using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public partial class PeopleSpawnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        EntityQuery peopleEntityQuery = EntityManager.CreateEntityQuery(typeof(PeopleTag));
        PeopleSpawnerComponent peopleSpawnerComponent = SystemAPI.GetSingleton<PeopleSpawnerComponent>();
        RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();

        EntityCommandBuffer entityCommandBuffer = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);
        
        if(peopleEntityQuery.CalculateEntityCount() < peopleSpawnerComponent.MaxNbPeople)
        {
            Entity spawnedEntity = entityCommandBuffer.Instantiate(peopleSpawnerComponent.PlayerPrefab);
            entityCommandBuffer.SetComponent(spawnedEntity, new Speed
            {
                Value = randomComponent.ValueRW.Random.NextFloat(10f, 50f)
            });
        }
    }
}
