using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PersonSpawnZoneAuthoring : MonoBehaviour
{
    public float SizeXZone;
    public float SizeZZone;

    public Color GizmosColor;

    private void OnDrawGizmos()
    {
        Gizmos.color = GizmosColor;
        Gizmos.DrawCube(transform.position, new Vector3(SizeXZone, 0.01f, SizeZZone));
    }
}

public class PersonSpawnZoneBaker : Baker<PersonSpawnZoneAuthoring>
{
    public override void Bake(PersonSpawnZoneAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new PersonSpawnZone
        {
            SpawnCenterZone = authoring.transform.position,
            SizeXZone = authoring.SizeXZone,
            SizeZZone = authoring.SizeZZone
        });
    }
}
