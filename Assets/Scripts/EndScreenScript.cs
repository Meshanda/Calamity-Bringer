using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    [Space(5)] 
    [Header("Audio")] 
    [SerializeField] private AudioClip _clickClip;
    [SerializeField] private AudioClip _victoryMusic;

    private void Start()
    {
        MusicManager.Instance.PutSound(MusicManager.AudioChannel.Music, _victoryMusic);
    }

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
    public void PlayClickSound()
    {
        MusicManager.Instance.PutSound(MusicManager.AudioChannel.Sound, _clickClip);
    }
}
