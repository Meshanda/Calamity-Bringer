using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    public void ClickReplay()
    {
        SceneManager.LoadScene(SceneReferences.GAME_SCENE);
    }
    public void ClickMenu()
    {
        SceneManager.LoadScene(SceneReferences.MAIN_MENU);
    }
    public void ClickQuit()
    {
        Utils.QuitGame();
    }
}
