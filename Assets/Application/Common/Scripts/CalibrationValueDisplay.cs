using Obvious.Soap;
using System;
using UnityEngine;

public class CalibrationValueDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI text;
    public FloatVariable CalibrationValue;

    private string MetersToCentimeters(float value) 
        => (value * 100).ToString("00") + " cm";

    private void OnEnable()
    {
        CalibrationValue.OnValueChanged += OnCalibrationValueChanged;
        
        SetText($"{MetersToCentimeters(CalibrationValue.Value)}");
    }

    private void OnDisable()
    {
        CalibrationValue.OnValueChanged -= OnCalibrationValueChanged;
    }

    private void OnCalibrationValueChanged(float obj)
    {
        SetText($"{MetersToCentimeters(obj)}");
    }

    private void SetText(string text)
    {
        this.text.text = text;
    }
}
