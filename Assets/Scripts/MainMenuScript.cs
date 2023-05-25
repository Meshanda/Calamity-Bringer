using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [Serializable]
    public enum MenuState
    {
        Main,
        Config,
        Settings
    }
    
    [Header("Cameras")] 
    [SerializeField] private GameObject _mainCam;
    [SerializeField] private GameObject _configCam;
    [SerializeField] private GameObject _settingsCam;
    
    [Space(10)]
    [Header("Canvases")] 
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _configCanvas;
    [SerializeField] private GameObject _settingsCanvas;
    
    [Space(10)]
    [Header("Audio")]
    [SerializeField] private AudioClip _clickSound;

    private void Start()
    {
        SwitchMenuState(MenuState.Main);
    }

    #region Main Canvas

    public void ClickPlay()
    {
        SceneManager.LoadScene(SceneReferences.GAME_SCENE);
    }

    public void ClickConfigure()
    {
        SwitchMenuState(MenuState.Config);
    }

    public void ClickSettings()
    {
        SwitchMenuState(MenuState.Settings);
    }
    
    public void ClickQuit()
    {
        Utils.QuitGame();
    }

    #endregion

    #region Utils

    public void SwitchMenuState(MenuState state)
    {
        _mainCam.SetActive(false);
        _mainCanvas.SetActive(false);
        
        _configCam.SetActive(false);
        _configCanvas.SetActive(false);
        
        _settingsCam.SetActive(false);
        _settingsCanvas.SetActive(false);
        
        switch (state)
        {
            case MenuState.Main:
                _mainCam.SetActive(true);
                _mainCanvas.SetActive(true);
                break;
            case MenuState.Config:
                _configCam.SetActive(true);
                _configCanvas.SetActive(true);
                break;
            case MenuState.Settings:
                _settingsCam.SetActive(true);
                _settingsCanvas.SetActive(true);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(state), state, null);
        }
    }

    public void PlayClickSound()
    {
        MusicManager.Instance.PutSound(MusicManager.AudioChannel.Sound, _clickSound);
    }

    #endregion
    
}
