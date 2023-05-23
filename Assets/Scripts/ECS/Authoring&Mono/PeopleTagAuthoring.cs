using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PeopleTagAuthoring : MonoBehaviour
{ }

public class PeopleTagBaker : Baker<PeopleTagAuthoring>
{
    public override void Bake(PeopleTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent<PeopleTag>(entity);
    }
}
