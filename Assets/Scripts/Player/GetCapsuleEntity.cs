using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.VisualScripting;
using UnityEngine;

public  class GetCapsuleEntity : MonoBehaviour
{
    public static GetCapsuleEntity Instance;

    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
}
