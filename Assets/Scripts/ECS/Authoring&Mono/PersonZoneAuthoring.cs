using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PersonZoneAuthoring : MonoBehaviour
{
    public float SizeXZone;
    public float SizeZZone;

    public bool DrawGizmos;
    public Color GizmosColor;

    private void OnDrawGizmos()
    {
        if (!DrawGizmos) return;

        Gizmos.color = GizmosColor;
        Gizmos.DrawCube(transform.position, new Vector3(SizeXZone, 0.01f, SizeZZone));
    }
}

public class PersonZoneBaker : Baker<PersonZoneAuthoring>
{
    public override void Bake(PersonZoneAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new PersonZone
        {
            SpawnCenterZone = authoring.transform.position,
            SizeXZone = authoring.SizeXZone,
            SizeZZone = authoring.SizeZZone
        });
    }
}
