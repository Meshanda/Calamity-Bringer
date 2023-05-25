using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BandageBuilding : MonoBehaviour
{
    [SerializeField] private int _points;
    [SerializeField] private GameManager _gm; 
    // Start is called before the first frame update
    public void OnCollisionEnter(Collision collision)
    {
        _gm.GainPoints(_points);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + " a");
        _gm.GainPoints(_points);
        Debug.Log(this+" "+gameObject.name);
        Destroy(gameObject);
    }
}
