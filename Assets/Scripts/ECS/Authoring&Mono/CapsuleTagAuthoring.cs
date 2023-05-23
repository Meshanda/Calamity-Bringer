using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class CapsuleTagAuthoring : MonoBehaviour
{
    public Transform player;
}

public class CapsuleTagBaker : Baker<CapsuleTagAuthoring>
{
    public override void Bake(CapsuleTagAuthoring authoring)
    {
        var tombstoneEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent<CapsuleTag>(tombstoneEntity);
    }
}
