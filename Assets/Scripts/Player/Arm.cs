using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    [SerializeField] private PlayerAttack _playerAttack;

    private void OnTriggerEnter(Collider otherCollider)
    {  
        _playerAttack.OnArmTriggerEnter(otherCollider);
    }
}
