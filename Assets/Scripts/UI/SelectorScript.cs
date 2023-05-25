using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectorScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private GameObject _container;
    [SerializeField] private int _maxElements;
    [SerializeField] private bool _canBeNull;
    
    private int _index;
    public int Index => _index;

    public void Init(int newIndex)
    {
        _index = newIndex;
        ActivePart();
    }
    
    public void ClickNext()
    {
        _index++;
        if (_index > _maxElements)
            _index = _canBeNull ? -1 : 0;

        ActivePart();
    }

    public void ClickPrevious()
    {
        _index--;
        _index = _index switch
        {
            -1 => _canBeNull ? -1 : _maxElements,
            -2 => _maxElements,
            _ => _index
        };

        ActivePart();
    }

    private void ActivePart()
    {
        foreach (Transform child in _container.transform)
        {
            child.gameObject.SetActive(false);
        }

        if (_index < 0)
        {
            _label.text = "Empty";
            return;
        }
        
        var currentPart = _container.transform.GetChild(_index).gameObject;
        currentPart.SetActive(true);
        _label.text = currentPart.name;
    }
}
