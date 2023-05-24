using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class PeopleSpawnerAuthoring : MonoBehaviour
{
    public float PeopleMinSpeed;
    public float PeopleMaxSpeed;
    public int MaxNbPeople;
}

public class PeopleSpawnerBaker : Baker<PeopleSpawnerAuthoring>
{
    public override void Bake(PeopleSpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new PersonSpawnerComponent
        {
            PeopleMinSpeed = authoring.PeopleMinSpeed,
            PeopleMaxSpeed = authoring.PeopleMaxSpeed,

            MaxNbPeople = authoring.MaxNbPeople,
        });
    }
}