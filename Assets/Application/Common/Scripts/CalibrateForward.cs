using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;

public class CalibrateForward : MonoBehaviour
{
    [Header("Variables")]
    public Vector3Variable headPosition;
    public Vector3Variable headForward;
    public Vector3Variable initialHeadPosition;
    public Vector3Variable initialHeadForward;
    [Space(10)]
    public FloatVariable forwardDistance;
    public FloatVariable currentDistance;

    [Header("Events")]
    public UnityEvent onCalibrateForward;

    private bool isCalibrating = false;

    public void Calibrate(bool pressed)
    {
        if (!pressed || !isCalibrating)
            return;

        Debug.Log($"Forwards Calibration {currentDistance.Value}");

        Vector3 offset = headPosition.Value - initialHeadPosition.Value;
        currentDistance.Value = Vector3.Dot(initialHeadForward.Value, offset);
        forwardDistance.Value = Mathf.Max(forwardDistance.Value, currentDistance.Value);

        onCalibrateForward.Invoke();
    }

    public void StartCalibrating()
    {
        Debug.Log("STARTED CALIBRATING FORWARDS");

        initialHeadForward.Value = headForward.Value;
        initialHeadPosition.Value = headPosition.Value;
        isCalibrating = true;
    }

    public void StopCalibrating()
    {
        isCalibrating = false;
    }
}
