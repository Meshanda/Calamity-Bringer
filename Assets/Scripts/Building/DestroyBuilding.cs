using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class DestroyBuilding : MonoBehaviour
{
    [SerializeField] private float _force;
    [SerializeField] private float _childGravityMultiplier;

    public UnityEvent OnExplode;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player") 
    //    {
    //        Explode();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Explode(other.gameObject.transform);
        }
    }

    public void Explode( Transform other) 
    {
        BoxCollider bc = gameObject.GetComponent<BoxCollider>();
        

        BoxCollider[] childs = GetComponentsInChildren<BoxCollider>();
        foreach(var child in childs) 
        {
            child.isTrigger = false;
            if (child.gameObject.TryGetComponent(out MovementDebris movDebris))
                movDebris.GravityMultiplier = _childGravityMultiplier;
        }

        Rigidbody[] rigidbodys= GetComponentsInChildren<Rigidbody>();
        foreach (var child in rigidbodys)
        {
            child.useGravity = true;
            child.AddForce((child.position - other.position).normalized* _force);
        }
        
        bc.isTrigger = true;

        OnExplode.Invoke();
    }
}
