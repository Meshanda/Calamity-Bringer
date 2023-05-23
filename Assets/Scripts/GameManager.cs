using System;
using UnityEngine;
using ScriptableObjects.Variables;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager instance;
    public static GameManager Instance => instance;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(gameObject);

        instance = this;
    }
    #endregion

    [SerializeField] private IntVariable score;
    [HideInInspector] public UnityEvent OnScoreUpdate;

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
        score.value = 0;
        OnScoreUpdate.Invoke();
    }

    public void GainPoints(int points)
    {
        score.value += points;
        OnScoreUpdate.Invoke();
    }
    
    private void OnTimerFinished()
    {
        SceneManager.LoadSceneAsync(SceneReferences.END_SCENE, LoadSceneMode.Single)
            .completed += _ => Cursor.lockState = CursorLockMode.None;
    }
}
