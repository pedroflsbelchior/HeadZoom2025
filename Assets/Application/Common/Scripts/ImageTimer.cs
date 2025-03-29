using Obvious.Soap;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ImageTimer : MonoBehaviour
{
    public TrialDataVariable trialData;
    public TrialImageDataVariable currentTrialImageData;
    public FloatVariable timeEllapsed;
    [Header("Events")]
    public UnityEvent onTimeOut;


    private Coroutine timer;
    private bool isPaused = false;
    private float remainingTime;

    private void OnEnable()
    {
        currentTrialImageData.OnValueChanged += SetCurrentTrialImageData;
    }

    private void OnDisable()
    {
        currentTrialImageData.OnValueChanged -= SetCurrentTrialImageData;
    }

    IEnumerator TimeImage(int seconds)
    {
        remainingTime = seconds;
        timeEllapsed.Value = 0;

        while (remainingTime > 0)
        {
            if (!isPaused)
            {
                remainingTime -= Time.deltaTime;
                timeEllapsed.Value = timeEllapsed.Value + Time.deltaTime;
            }
            //Debug.Log($"remainingTime: {remainingTime}");
            yield return null;
        }

        onTimeOut.Invoke();
    }

    public void StartTimer()
    {
        // Null checks
        if (timer != null)
        {
            Debug.Log("Timer already running");
            return;
        }
        if (trialData == null || trialData.Value == null)
            return;
        if (currentTrialImageData == null || currentTrialImageData.Value == null)
            return;

        // Check if the time is valid
        if (trialData.Value.secondsPerImage <= 0)
        {
            Debug.Log("Cannot set timer to a value of 0 or less");
            return;
        }

        timer = StartCoroutine(TimeImage(trialData.Value.secondsPerImage));
    }

    public void StopTimer()
    {
        if (timer == null)
            return;
        StopCoroutine(timer);
        timer = null;
    }

    private void SetCurrentTrialImageData(TrialImageData data)
    {
        if (data == null)
            return;
        StartTimer();
    }

    public void PauseTimer()
    {
        if (timer != null)
            isPaused = true;
    }

    public void ResumeTimer()
    {
        isPaused = false;
    }
}
