using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BuildIngTagAuthoring : MonoBehaviour
{
     public static int index;
     public int trueIndex;
     public List<GameObject> objects;

    public void Increment()
    {
        gameObject.AddComponent<BoxCollider>();
        ColliderDestroyerSingleton.Instance.buildings.Add(this);
        trueIndex = index;
    }
}
public class BuildIngTagBaker : Baker<BuildIngTagAuthoring>
{
    public override void Bake(BuildIngTagAuthoring authoring)
    {
        var tombstoneEntity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent<BuildingTag>(tombstoneEntity, new BuildingTag
        {
            index = BuildIngTagAuthoring.index
        }) ;
        authoring.Increment();
        BuildIngTagAuthoring.index++;
    }
}


