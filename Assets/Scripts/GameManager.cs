using System;
using System.Collections.Generic;
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
    
    [Space(10)]
    [Header("Canvas")]
    [SerializeField] private GameObject _pauseCanvas;
    [SerializeField] private GameObject _hudCanvas;
    
    [Space(10)]
    [SerializeField] private List<GameObject> _objectToDisableOnVictory;

    public static event Action GameUnpaused;

    private void OnEnable()
    {
        TimerSystem.TimerFinished += OnTimerFinished;
        PauseSystem.GamePaused += OnGamePaused;
    }

    private void OnDisable()
    {
        TimerSystem.TimerFinished -= OnTimerFinished;
        PauseSystem.GamePaused -= OnGamePaused;
    }

    private void OnGamePaused()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        
        _pauseCanvas.SetActive(true);
        _hudCanvas.SetActive(false);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        score.value = 0;
        OnScoreUpdate.Invoke();

        _hudCanvas.SetActive(true);
        _pauseCanvas.SetActive(false);
    }

    public void GainPoints(int points)
    {
        score.value += points;
        OnScoreUpdate.Invoke();
    }
    
    private void OnTimerFinished()
    {
        _objectToDisableOnVictory.ForEach(o => o.SetActive(false));
        
        SceneManager.LoadSceneAsync(SceneReferences.END_SCENE, LoadSceneMode.Additive)
            .completed += _ => Cursor.lockState = CursorLockMode.None;
    }

    public void ClickUnpause()
    {
        UnPause();
    }

    private void UnPause()
    {
        GameUnpaused?.Invoke();
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        _pauseCanvas.SetActive(false);
        _hudCanvas.SetActive(true);
    }

    public void ClickMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneReferences.MAIN_MENU);
    }
}
