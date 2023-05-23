using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct PeopleSpawnerComponent : IComponentData
{
    public int MaxNbPeople;
    public Entity PlayerPrefab;
}
