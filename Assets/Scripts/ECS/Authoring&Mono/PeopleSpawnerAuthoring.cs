using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PeopleSpawnerAuthoring : MonoBehaviour
{
    public float PeopleMinSpeed;
    public float PeopleMaxSpeed;
    [Space(10)]
    public int MaxNbPeople;
    public GameObject PlayerPrefab;
}

public class PeopleSpawnerBaker : Baker<PeopleSpawnerAuthoring>
{
    public override void Bake(PeopleSpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new PeopleSpawnerComponent
        {
            PeopleMinSpeed = authoring.PeopleMinSpeed,
            PeopleMaxSpeed = authoring.PeopleMaxSpeed,

            MaxNbPeople = authoring.MaxNbPeople,
            PlayerPrefab = GetEntity(authoring.PlayerPrefab, TransformUsageFlags.Dynamic)
        });
    }
}