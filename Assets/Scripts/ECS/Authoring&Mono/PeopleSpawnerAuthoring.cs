using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PeopleSpawnerAuthoring : MonoBehaviour
{
    public Vector3 SpawnCenterZone;
    public float SizeXZone;
    public float SizeZZone;

    public float PeopleMinSpeed;
    public float PeopleMaxSpeed;

    public int MaxNbPeople;
    public GameObject PlayerPrefab;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1.0f, 0.64f, 0.0f);
        Gizmos.DrawCube(SpawnCenterZone, new Vector3(SizeXZone, 0.01f, SizeZZone));
    }
}

public class PeopleSpawnerBaker : Baker<PeopleSpawnerAuthoring>
{
    public override void Bake(PeopleSpawnerAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new PeopleSpawnerComponent
        {
            SpawnCenterZone = authoring.SpawnCenterZone,
            SizeXZone = authoring.SizeXZone,
            SizeZZone = authoring.SizeZZone,

            PeopleMinSpeed = authoring.PeopleMinSpeed,
            PeopleMaxSpeed = authoring.PeopleMaxSpeed,

            MaxNbPeople = authoring.MaxNbPeople,
            PlayerPrefab = GetEntity(authoring.PlayerPrefab, TransformUsageFlags.Dynamic)
        });
    }
}