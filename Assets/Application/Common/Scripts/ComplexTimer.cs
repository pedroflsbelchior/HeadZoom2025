using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ComplexTimer : MonoBehaviour
{
    private enum StartTime { OnAwake, OnEnable, OnStart }

    [Header("Settings")]
    [SerializeField] private float seconds = 5f;
    [SerializeField] private StartTime startTime = StartTime.OnEnable;
    [Header("Events")]
    [SerializeField] private UnityEvent onStart = new UnityEvent();
    [SerializeField] private UnityEvent<float> onProgress = new UnityEvent<float>();
    [SerializeField] private UnityEvent onFinish = new UnityEvent();

    private Coroutine timer = null;

    private IEnumerator Countdown(float seconds)
    {
        onStart.Invoke();

        if (seconds <= 0)
        {
            onProgress.Invoke(1);
            onFinish.Invoke();
            yield break;
        }

        float time = 0f;

        while (true)
        {
            onProgress.Invoke(Mathf.Clamp01(time / seconds));

            if (time >= seconds)
            {
                onFinish.Invoke();
                yield break;
            }

            time += Time.deltaTime;
            yield return null;
        }
    }



    private void StartTimer()
    {
        if (timer != null)
            StopTimer();
        timer = StartCoroutine(Countdown(seconds));
    }

    private void StopTimer()
    {
        if (timer == null)
            return;

        StopCoroutine(timer);
        timer = null;
    }

    private void OnEnable()
    {
        if (startTime == StartTime.OnEnable)
            StartTimer();
    }

    private void OnDisable()
    {
        StopTimer();
    }

    private void Awake()
    {
        if (startTime == StartTime.OnAwake)
            StartTimer();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (startTime == StartTime.OnStart)
            StartTimer();
    }

    private void OnDestroy()
    {
        StopTimer();
    }
}
