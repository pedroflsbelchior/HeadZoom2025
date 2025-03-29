using Obvious.Soap;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SimpleTimer : MonoBehaviour
{
    public UnityEvent onTimerComplete;

    Coroutine timer;
    
    public void StartTimer(float duration)
    {
        if (timer != null)
            return;
        timer = StartCoroutine(Timer(duration));
    }

    public void StopTimer()
    {
        if (timer == null)
            return;
        StopCoroutine(timer);
        timer = null;
    }

    IEnumerator Timer(float duration)
    {
        yield return new WaitForSeconds(duration);
        onTimerComplete.Invoke();
    }
}
