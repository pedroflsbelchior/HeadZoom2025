using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;

public class CalibrateBackward : MonoBehaviour
{
    [Header("Variables")]
    public Vector3Variable headPosition;
    public Vector3Variable headForward;
    public Vector3Variable initialHeadPosition;
    public Vector3Variable initialHeadForward;
    [Space(10)]
    public FloatVariable backwardDistance;
    public FloatVariable currentDistance;

    [Header("Events")]
    public UnityEvent onCalibrateBackward;

    private bool isCalibrating = false;

    public void Calibrate(bool pressed)
    {
        if (!pressed || !isCalibrating)
            return;

        Debug.Log($"Backwards Calibration {currentDistance.Value}");

        Vector3 offset = headPosition.Value - initialHeadPosition.Value;
        currentDistance.Value = Vector3.Dot(initialHeadForward.Value, offset);
        backwardDistance.Value = Mathf.Abs(Mathf.Min(-backwardDistance.Value, currentDistance.Value));

        onCalibrateBackward.Invoke();
    }

    public void StartCalibrating()
    {
        //initialHeadForward.Value = headForward.Value;
        //initialHeadPosition.Value = headPosition.Value;
        isCalibrating = true;
    }

    public void StopCalibrating()
    {
        isCalibrating = false;
    }
}
