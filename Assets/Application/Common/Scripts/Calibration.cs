using Obvious.Soap;
using System;
using UnityEngine;

public class Calibration : MonoBehaviour
{
    public float DefaultForwardDistance = 0.05f;
    public float DefaultBackwardDistance = 0.05f;

    [Header("Head Calibration")]
    public Vector3Variable headPosition;
    public Vector3Variable headForward;
    public Vector3Variable headDefaultPosition;
    public Vector3Variable headDefaultForward;

    [Header("Head Distance")]
    public FloatVariable headCurrentDistance;
    public FloatVariable headFowardDistance;
    public FloatVariable headBackwardDistance;
    
    [Header("Events")]
    public ScriptableEventNoParam CalibrationStart;
    public ScriptableEventNoParam CalibrationStop;
    public ScriptableEventNoParam CalibrationCancel;
    public ScriptableEventNoParam CalibrationReset;

    [Header("Debug")]
    public bool calibrating = false;
    public float initialValueFowardDistance = 0;
    public float initialValueBackwardDistance = 0;

    private void OnEnable()
    {
        CalibrationStart.OnRaised += OnCalibrationStart;
        CalibrationStop.OnRaised += OnCalibrationStop;
        CalibrationCancel.OnRaised += OnCalibrationCancel;
        CalibrationReset.OnRaised += ResetCalibration;

        headPosition.OnValueChanged += OnHeadPositionChanged;
        headForward.OnValueChanged += OnHeadForwardChanged;
    }
    private void OnDisable()
    {
        CalibrationStart.OnRaised -= OnCalibrationStart;
        CalibrationStop.OnRaised -= OnCalibrationStop;
        CalibrationCancel.OnRaised -= OnCalibrationCancel;
        CalibrationReset.OnRaised -= ResetCalibration;
    }

    private void OnHeadPositionChanged(Vector3 vector)
    {
        Calibrate();
    }
    private void OnHeadForwardChanged(Vector3 vector)
    {
        Calibrate();
    }
    public void Calibrate()
    {
        if (!calibrating) return;

        Vector3 offset = headPosition.Value - headDefaultPosition.Value;
        headCurrentDistance.Value = Vector3.Dot(headDefaultForward.Value, offset);
        //Debug.Log($"Distance: {distance}. Offset: {offset}, Max {Mathf.Max(headFowardDistance.Value, distance)}");
        headFowardDistance.Value = Mathf.Max(headFowardDistance.Value, headCurrentDistance.Value);
        headBackwardDistance.Value = Mathf.Abs(Mathf.Min(-headBackwardDistance.Value, headCurrentDistance.Value));
    }

    public void OnCalibrationStart()
    {
        calibrating = true;
        initialValueBackwardDistance = headBackwardDistance.Value;
        initialValueFowardDistance = headFowardDistance.Value;

        headDefaultPosition.Value = headPosition.Value;
        headDefaultForward.Value = headForward.Value;
    }

    public void OnCalibrationCancel()
    {
        calibrating = false;
        headBackwardDistance.Value = initialValueBackwardDistance;
        headFowardDistance.Value = initialValueFowardDistance;
    }

    public void OnCalibrationStop()
    {
        calibrating = false;
    }

    public void ResetCalibration()
    {
        headDefaultPosition.Value = headPosition.Value;
        headDefaultForward.Value = headForward.Value;

        headFowardDistance.Value = DefaultForwardDistance;
        headBackwardDistance.Value = DefaultBackwardDistance;
    }
}
