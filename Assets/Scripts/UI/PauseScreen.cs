using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private AudioClip _clickSound;
    public void PlayClickSound()
    {
        MusicManager.Instance.PutSound(MusicManager.AudioChannel.Sound, _clickSound);
    }
}
