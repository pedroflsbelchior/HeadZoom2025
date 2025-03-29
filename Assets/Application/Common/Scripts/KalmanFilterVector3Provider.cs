using Obvious.Soap;
using System;
using UnityEngine;

public class KalmanFilterVector3Provider : MonoBehaviour
{
    public Vector3Variable input;
    [Space(10)]
    public Vector3Variable output;
    [Header("Parameters")]
    public FloatVariable Q;
    public FloatVariable R;
    [Space(10)]
    public TrialImageDataVariable trialImageData;

    private KalmanFilterVector3 filter;

    private void OnEnable()
    {
        InitializeFilter();

        trialImageData.OnValueChanged += OnTrialImageDataChanged;
        input.OnValueChanged += OnInputValueChanged;
        Q.OnValueChanged += OnQValueChanged;
        R.OnValueChanged += OnRValueChanged;
    }

    private void OnDisable()
    {
        trialImageData.OnValueChanged -= OnTrialImageDataChanged;
        input.OnValueChanged -= OnInputValueChanged;
        Q.OnValueChanged -= OnQValueChanged;
        R.OnValueChanged -= OnRValueChanged;
    }

    private void OnQValueChanged(float obj) => InitializeFilter();
    private void OnRValueChanged(float obj) => InitializeFilter();

    private void InitializeFilter()
    {
        if (Q.Value == 0 || R.Value == 0)
            filter = null;
        else
            filter = new KalmanFilterVector3(Q.Value, R.Value);
    }



    private void OnTrialImageDataChanged(TrialImageData data)
    {
        if (filter == null)
            return;
        filter.Reset();
    }

    private void OnInputValueChanged(Vector3 value)
    {   
        if (filter == null)
            return;
        output.Value = filter.Update(value, Q.Value, R.Value);
    }
}
