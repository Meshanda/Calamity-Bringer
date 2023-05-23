using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    private bool hasAlreadyScored;
    [SerializeField] private int points;

    public void Score()
    {
        if (hasAlreadyScored) return;

        GameManager.Instance.GainPoints(points);
        hasAlreadyScored = true;
    }
}
