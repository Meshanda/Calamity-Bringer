using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    [SerializeField] private float _delay = 1f;
    private void Start()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOScale(1.05f, _delay));
        sequence.Append(transform.DOScale(.95f, _delay));
        sequence.SetLoops(-1);
        sequence.SetEase(Ease.Linear);
    }
}
