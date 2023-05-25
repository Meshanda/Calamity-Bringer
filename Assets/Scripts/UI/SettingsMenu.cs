using System;
using ScriptableObjects.Variables;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private MainMenuScript _mainMenuScript;

    [Header("General")] 
    [SerializeField] private FloatVariable _timerMaxVariable;
    [SerializeField] private TMP_InputField _timerInputField;
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;

    [Header("Audio")] 
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private FloatVariable _musicVolume;
    [Space(5)]
    [SerializeField] private Slider _soundSlider;
    [SerializeField] private FloatVariable _soundVolume;

    private void Start()
    {
        UpdateInputTimer();
        InitAudioSliders();
    }

    private void InitAudioSliders()
    {
        _musicSlider.value = _musicVolume.value;
        _soundSlider.value = _soundVolume.value;
    }

    public void OnTimerInputChanged(string newStr)
    {
        var newValue = int.Parse(newStr);
        _timerMaxVariable.value = Mathf.Clamp(newValue, _minValue, _maxValue);
        UpdateInputTimer();
    }

    private void UpdateInputTimer()
    {
        _timerInputField.text = _timerMaxVariable.value.ToString();
    }

    public void ClickBack()
    {
        _mainMenuScript.SwitchMenuState(MainMenuScript.MenuState.Main);
    }

    public void OnMusicVolumeChanged(float value)
    {
        MusicManager.Instance.ChangeVolume(MusicManager.AudioChannel.Music, value);
    }
    public void OnSoundVolumeChanged(float value)
    {
        MusicManager.Instance.ChangeVolume(MusicManager.AudioChannel.Sound, value);
    }
}
