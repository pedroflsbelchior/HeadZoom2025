using Obvious.Soap;
using System;
using UnityEngine;

public class TrialDataProvider : MonoBehaviour
{
    [Header("Trial Data")]
    public TrialDataVariable trialDataVariable;

    public IntVariable SecondsPerImageVariable;
    public IntVariable ImagesPerAnchorVariable;
    public FloatVariable HitRadiusVariable;

    private void OnEnable()
    {
        trialDataVariable.OnValueChanged += OnTrialDataChanged;
    }

    private void OnDisable()
    {
        trialDataVariable.OnValueChanged -= OnTrialDataChanged;
    }

    private void OnTrialDataChanged(TrialData data)
    {
        if (data == null)
            return;
        SecondsPerImageVariable.Value = data.secondsPerImage;
        ImagesPerAnchorVariable.Value = data.imagesPerAnchor;
        HitRadiusVariable.Value = data.hitRadius;
    }
}
