using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class DestroyBuilding : MonoBehaviour
{
    [SerializeField] private float _force;
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player") 
    //    {
    //        Explode();
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("a");
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

        }

        Rigidbody[] rigidbodys= GetComponentsInChildren<Rigidbody>();
        foreach (var child in rigidbodys)
        {
            child.useGravity = true;
            child.AddForce((child.position - other.position).normalized* _force);

        }
        bc.isTrigger = true;
    }
}
