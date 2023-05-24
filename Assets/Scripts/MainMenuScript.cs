using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [Header("Cameras")] 
    [SerializeField] private GameObject _mainCam;
    [SerializeField] private GameObject _configCam;
    
    [Space(10)]
    [Header("Canvases")] 
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _configCanvas;

    private void Start()
    {
        ToggleMainCanvas(true);
    }

    #region Main Canvas

    public void ClickPlay()
    {
        SceneManager.LoadScene(SceneReferences.GAME_SCENE);
    }

    public void ClickConfigure()
    {
        ToggleMainCanvas(false);
    }
    
    public void ClickQuit()
    {
        Utils.QuitGame();
    }

    #endregion

    #region Utils

    public void ToggleMainCanvas(bool toggle)
    {
        _mainCam.SetActive(toggle);
        _mainCanvas.SetActive(toggle);
        
        _configCam.SetActive(!toggle);
        _configCanvas.SetActive(!toggle);
    }

    #endregion
    
}
