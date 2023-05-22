using System;
using System.Collections;
using UnityEngine;

public static class Utils
{
    public static IEnumerator DelayRoutine(float delay, Action action)
    {
        yield return new WaitForSeconds(delay);
        
        action?.Invoke();
    }
}