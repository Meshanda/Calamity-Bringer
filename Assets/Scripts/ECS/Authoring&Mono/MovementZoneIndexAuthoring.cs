using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class MovementZoneIndexAuthoring : MonoBehaviour
{
    public int MovementIndex;
}

public class MovementZoneIndexBaker : Baker<MovementZoneIndexAuthoring>
{
    public override void Bake(MovementZoneIndexAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new MovementZoneIndex
        {
            MovementIndex = authoring.MovementIndex
        });
    }
}
