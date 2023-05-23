using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct PeopleSpawnerComponent : IComponentData
{
    public float3 SpawnCenterZone;
    public float SizeXZone;
    public float SizeZZone;

    public float PeopleMinSpeed;
    public float PeopleMaxSpeed;

    public int MaxNbPeople;
    public Entity PlayerPrefab;
}
