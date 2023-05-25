using System;
using ScriptableObjects.Variables;
using TMPro;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private MainMenuScript _mainMenuScript;

    [Header("General")] 
    [SerializeField] private FloatVariable _timerMaxVariable;
    [SerializeField] private TMP_InputField _timerInputField;
    [SerializeField] private float _minValue;
    [SerializeField] private float _maxValue;

    private void Start()
    {
        UpdateInputTimer();
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
}
