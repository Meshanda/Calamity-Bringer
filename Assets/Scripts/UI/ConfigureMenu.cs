using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigureMenu : MonoBehaviour
{
    [SerializeField] private MainMenuScript _mainMenuScript;
    [SerializeField] private float _rotateMultiplier;
    [SerializeField] private Transform _playerModel;
    [SerializeField] private PlayerSkinData _playerSkinData;
    [SerializeField] private PlayerSkinSetter _playerSkinSetter;

    [Space(10)]
    [Header("Selectors")]
    [SerializeField] private SelectorScript _bodySelector;
    [SerializeField] private SelectorScript _bodyPartsSelector;
    [SerializeField] private SelectorScript _eyesSelector;
    [SerializeField] private SelectorScript _glovesSelector;
    [SerializeField] private SelectorScript _hatsSelector;
    [SerializeField] private SelectorScript _mouthSelector;
    [SerializeField] private SelectorScript _tailsSelector;

    private void Start()
    {
        _bodySelector.Init(_playerSkinData.BodyIndex);
        _bodyPartsSelector.Init(_playerSkinData.BodypartIndex);
        _eyesSelector.Init(_playerSkinData.EyeIndex);
        _glovesSelector.Init(_playerSkinData.GloveIndex);
        _hatsSelector.Init(_playerSkinData.HeadIndex);
        _mouthSelector.Init(_playerSkinData.MouthIndex);
        _tailsSelector.Init(_playerSkinData.TailIndex);
    }

    public void ClickValidate()
    {
        _playerSkinData.BodyIndex = _bodySelector.Index;
        _playerSkinData.BodypartIndex = _bodyPartsSelector.Index;
        _playerSkinData.EyeIndex = _eyesSelector.Index;
        _playerSkinData.GloveIndex = _glovesSelector.Index;
        _playerSkinData.HeadIndex = _hatsSelector.Index;
        _playerSkinData.MouthIndex = _mouthSelector.Index;
        _playerSkinData.TailIndex = _tailsSelector.Index;
        
        _mainMenuScript.SwitchMenuState(MainMenuScript.MenuState.Main);
    }

    public void ClickCancel()
    {
        _playerSkinSetter.SetSkin();
        _mainMenuScript.SwitchMenuState(MainMenuScript.MenuState.Main);
    }

    public void ClickRotateRight()
    {
        _playerModel.Rotate(Vector3.down, Time.deltaTime*_rotateMultiplier);
    }

    public void ClickRotateLeft()
    {
        _playerModel.Rotate(Vector3.up, Time.deltaTime*_rotateMultiplier);
    }
}
