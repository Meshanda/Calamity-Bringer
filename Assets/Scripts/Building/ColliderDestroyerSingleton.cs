using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderDestroyerSingleton : MonoBehaviour
{
    public static ColliderDestroyerSingleton Instance;
    public List<BuildIngTagAuthoring> buildings =new List<BuildIngTagAuthoring>();
    [SerializeField] private GameObject _collider;
    public static event Action<GameObject> BuildingDestroyed;

    public void Awake()
    {
        Instance = this; 
    }

    public void DestroyCollider(int index)
    {
        foreach(BuildIngTagAuthoring tag in buildings) 
        {
            if (tag.trueIndex == index)
            {
                BuildingDestroyed?.Invoke(tag.gameObject);
                Collider coll = tag.gameObject.GetComponent<Collider>();

                Destroy(coll);
            }
        }
    }
}
