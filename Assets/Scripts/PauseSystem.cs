using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    public static event Action GamePaused;

    public void OnPause()
    {
        GamePaused?.Invoke();
    }
}
