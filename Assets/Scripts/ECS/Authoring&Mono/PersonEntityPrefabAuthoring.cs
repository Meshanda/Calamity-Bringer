using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PersonEntityPrefabAuthoring : MonoBehaviour
{
    public GameObject PersonPrefab;
}

public class PersonEntityPrefabBaker : Baker<PersonEntityPrefabAuthoring>
{
    public override void Bake(PersonEntityPrefabAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new PersonPrefab
        {
            Value = GetEntity(authoring.PersonPrefab, TransformUsageFlags.Dynamic)
        });
    }
}
