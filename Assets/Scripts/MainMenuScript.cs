using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void ClickPlay()
    {
        SceneManager.LoadScene(SceneReferences.GAME_SCENE);
    }
    
    public void ClickQuit()
    {
        Utils.QuitGame();
    }
}
