using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private void OnEnable()
    {
        TimerSystem.TimerFinished += OnTimerFinished;
    }

    private void OnDisable()
    {
        TimerSystem.TimerFinished -= OnTimerFinished;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    
    private void OnTimerFinished()
    {
        SceneManager.LoadSceneAsync(SceneReferences.END_SCENE, LoadSceneMode.Single)
            .completed += _ => Cursor.lockState = CursorLockMode.None;
    }
}
