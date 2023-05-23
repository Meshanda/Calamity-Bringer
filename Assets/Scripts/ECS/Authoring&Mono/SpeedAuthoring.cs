using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class SpeedAuthoring : MonoBehaviour
{
    public float Value;
}

public class SpeedBaker : Baker<SpeedAuthoring>
{
    public override void Bake(SpeedAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new Speed { Value = authoring.Value });
    }
}
