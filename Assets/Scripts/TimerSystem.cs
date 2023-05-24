using System;
using ScriptableObjects.Variables;
using UnityEngine;
using UnityEngine.Events;

public class TimerSystem : MonoBehaviour
{
    [SerializeField] private FloatVariable _timerVariable;
    [SerializeField] private float _timerMax;

    public UnityEvent OnTimerReset;
    public static event Action TimerFinished;

    private void Start()
    {
        _timerVariable.value = _timerMax;
        OnTimerReset.Invoke();
    }

    private void Update()
    {
        _timerVariable.value -= Time.deltaTime;

        if (_timerVariable.value <= 0)
        {
            TimerFinished?.Invoke();
            Destroy(this);
        }
    }
}
