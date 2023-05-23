using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody), typeof(BoxCollider))]
public class MovementDebris : MonoBehaviour
{
    private Rigidbody _rb;
    private BoxCollider _bc;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _bc = GetComponent<BoxCollider>();
    }

    // Update is called once per frame

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
            return;
        Debug.Log(collision.gameObject.name);
        _rb.useGravity = false;
        _bc.isTrigger = true;
        _rb.velocity = Vector3.zero;
        _rb.angularVelocity = Vector3.zero;
    }
}
