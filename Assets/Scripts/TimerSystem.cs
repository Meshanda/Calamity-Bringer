using System;
using ScriptableObjects.Variables;
using UnityEngine;

public class TimerSystem : MonoBehaviour
{
    [SerializeField] private FloatVariable _timerVariable;

    private void Start()
    {
        _timerVariable.value = 0;
    }

    private void Update()
    {
        _timerVariable.value += Time.deltaTime;
    }
}
