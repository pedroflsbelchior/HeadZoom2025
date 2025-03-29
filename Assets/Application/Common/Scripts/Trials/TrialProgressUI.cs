using Obvious.Soap;
using System;
using UnityEngine;
using UnityEngine.Events;

public class TrialProgressUI : MonoBehaviour
{
    public IntVariable CurrentImageIndex;
    public IntVariable TotalImages;

    public UnityEvent<string> onProgress;

    public void OnEnable()
    {
        UpdateProgress();
        CurrentImageIndex.OnValueChanged += UpdateProgress;
        TotalImages.OnValueChanged += UpdateProgress;
    }

    public void OnDisable()
    {
        CurrentImageIndex.OnValueChanged -= UpdateProgress;
        TotalImages.OnValueChanged -= UpdateProgress;
    }

    private void UpdateProgress(int obj)
    {
        UpdateProgress();
    }

    private void UpdateProgress()
    {
        onProgress.Invoke($"{CurrentImageIndex.Value + 1:00} / {TotalImages.Value:00}");
    }
}
