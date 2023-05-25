using System;
using UnityEngine;

public class PlayerSkinSetter : MonoBehaviour
{
    [SerializeField] private PlayerSkinData _playerSkinData;
    
    [Space(5)]
    [SerializeField] private GameObject _bodies;
    [SerializeField] private GameObject _bodyparts;
    [SerializeField] private GameObject _eyes;
    [SerializeField] private GameObject _gloves;
    [SerializeField] private GameObject _heads;
    [SerializeField] private GameObject _mouths;
    [SerializeField] private GameObject _tails;

    private void Start()
    {
        SetSkin();
    }

    public void SetSkin()
    {
        SetPart(_bodies, _playerSkinData.BodyIndex);
        SetPart(_bodyparts, _playerSkinData.BodypartIndex);
        SetPart(_eyes, _playerSkinData.EyeIndex);
        SetPart(_gloves, _playerSkinData.GloveIndex);
        SetPart(_heads, _playerSkinData.HeadIndex);
        SetPart(_mouths, _playerSkinData.MouthIndex);
        SetPart(_tails, _playerSkinData.TailIndex);
    }

    private void SetPart(GameObject container, int index)
    {
        foreach (Transform child in container.transform)
        {
            child.gameObject.SetActive(false);
        }

        if (index < 0) return;

        container.transform.GetChild(index).gameObject.SetActive(true);
    }
}
