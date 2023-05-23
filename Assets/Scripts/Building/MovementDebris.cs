using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(BoxCollider))]
public class MovementDebris : MonoBehaviour
{
    public float GravityMultiplier { get; set; }
    
    private Rigidbody _rb;
    private BoxCollider _bc;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _bc = GetComponent<BoxCollider>();
    }

    private void FixedUpdate()
    {
        if (!_rb.useGravity) return;
        
        _rb.AddForce(Vector3.down * GravityMultiplier);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != 7) return;
        
        _rb.useGravity = false;
        _bc.isTrigger = true;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
