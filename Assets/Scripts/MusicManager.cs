using System;
using ScriptableObjects.Variables;
using UnityEngine;

public class MusicManager : GenericSingleton<MusicManager>
{
    public enum AudioChannel
    {
        Music,
        Sound
    }
    
    [Header("Music")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private FloatVariable _musicVolume;
    
    [Space(5)]
    [Header("Sound")]
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private FloatVariable _soundVolume;

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        UpdateVolume();
    }

    public void PutSound(AudioChannel channel, AudioClip clip)
    {
        switch (channel)
        {
            case AudioChannel.Music:
                _musicSource.clip = clip;
                _musicSource.Play();
                break;
            case AudioChannel.Sound:
                _soundSource.clip = clip;
                _soundSource.Play();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(channel), channel, null);
        }
    }

    public void ChangeVolume(AudioChannel channel, float volume)
    {
        switch (channel)
        {
            case AudioChannel.Music:
                _musicVolume.value = volume;
                break;
            case AudioChannel.Sound:
                _soundVolume.value = volume;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(channel), channel, null);
        }
        
        UpdateVolume();
    }
    
    public void UpdateVolume()
    {
        _musicSource.volume = _musicVolume.value;
        _soundSource.volume = _soundVolume.value;
    }
}
