using System;
using UnityEngine;
using UnityEngine.Events;

public class TrialImageDataEvents : MonoBehaviour
{
    public TrialImageDataVariable trialImageData;
    [Header("Events")]
    public UnityEvent<Texture2D> OnTexture;

    public void OnEnable()
    {
        trialImageData.OnValueChanged += OnImageDataChange;
        OnTexture.Invoke(trialImageData.Value.data.image);
    }

    private void OnDisable()
    {
        trialImageData.OnValueChanged -= OnImageDataChange;
    }

    private void OnImageDataChange(TrialImageData imageData)
    {
        OnTexture.Invoke(imageData.data.image);
    }
}
