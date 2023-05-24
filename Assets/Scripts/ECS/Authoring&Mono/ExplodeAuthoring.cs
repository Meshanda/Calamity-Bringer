using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class ExplodeAuthoring : MonoBehaviour
{
}
public class ExplodeTagBaker : Baker<ExplodeAuthoring>
{
    public override void Bake(ExplodeAuthoring authoring)
    {
        var Entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent<ExplodeAuthoring>(Entity);
    }
}
