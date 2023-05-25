using System;
using System.Collections.Generic;
using UnityEngine;
using ScriptableObjects.Variables;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

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
    [Header("Audio")]
    [SerializeField] private AudioClip _gameMusic;
    [SerializeField] private AudioClip _pauseMusic;
    [SerializeField] private AudioClip _destroyedBuildingSound;
    

    public static event Action GameUnpaused;

    private void OnEnable()
    {
        TimerSystem.TimerFinished += OnTimerFinished;
        PauseSystem.GamePaused += OnGamePaused;
        ColliderDestroyerSingleton.BuildingDestroyed += OnBuildingDestroyed;
    }

    private void OnDisable()
    {
        TimerSystem.TimerFinished -= OnTimerFinished;
        PauseSystem.GamePaused -= OnGamePaused;
        ColliderDestroyerSingleton.BuildingDestroyed -= OnBuildingDestroyed;
    }

    private void OnBuildingDestroyed(GameObject building)
    {
        building.GetComponent<Scorer>().Score();
        MusicManager.Instance.PutSound(MusicManager.AudioChannel.Sound, _destroyedBuildingSound);
    }

    private void OnGamePaused()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        MusicManager.Instance.PutSound(MusicManager.AudioChannel.Music, _pauseMusic);

        _pauseCanvas.SetActive(true);
        _hudCanvas.SetActive(false);
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        score.value = 0;
        OnScoreUpdate.Invoke();
        MusicManager.Instance.PutSound(MusicManager.AudioChannel.Music, _gameMusic);

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
        _hudCanvas.SetActive(false);
        _pauseCanvas.SetActive(false);
        
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
        MusicManager.Instance.PutSound(MusicManager.AudioChannel.Music, _gameMusic);
        
        _pauseCanvas.SetActive(false);
        _hudCanvas.SetActive(true);
    }

    public void ClickMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneReferences.MAIN_MENU);
    }
}
