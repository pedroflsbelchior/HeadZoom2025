using Obvious.Soap;
using System;
using UnityEngine;

public class KalmanFilterParameterProvider : MonoBehaviour
{
    [Header("Input")]
    public FloatVariable valueDriver;
    [Header("Output")]
    public FloatVariable kalmanFilterQ;
    public FloatVariable kalmanFilterR;
    [Header("Logic")]
    public FilterData data;

    private void OnEnable()
    {
        valueDriver.OnValueChanged += OnValueChanged;
        OnValueChanged(valueDriver.Value);
    }

    private void OnDisable()
    {
        valueDriver.OnValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(float obj)
    {
        if (data == null)
            return;
        var values = data.CalculateParameters(obj);
        kalmanFilterQ.Value = values.Q;
        kalmanFilterR.Value = values.R;
    }

    public void SetData(FilterData data)
    {
        if (data == null)
            return;
        if (data == this.data)
            return;

        this.data = data;
        OnValueChanged(valueDriver.Value);
    }
}
