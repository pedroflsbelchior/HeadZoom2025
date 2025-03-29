using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;

public enum CalibrationStep
{
    None,
    Forward,
    Backward,
    Done
}

public class SteppedCalibration : MonoBehaviour
{
    [Header("Variables")]
    public Vector3Variable headPosition;
    public Vector3Variable headForward;
    public Vector3Variable initialHeadPosition;
    public Vector3Variable initialHeadForward;
    [Space(10)]
    public FloatVariable forwardDistance;
    public FloatVariable backwardDistance;
    public FloatVariable currentDistance;
    [Space(10)]
    public BoolVariable leftControllerTrigger;
    public BoolVariable rightControllerTrigger;
    public BoolVariable leftDoublePinch;
    public BoolVariable rightDoublePinch;
    [Header("Events")]
    public UnityEvent onCalibrationStarted;
    public UnityEvent onCalibratedForward;
    public UnityEvent onCalibratedBackward;
    public UnityEvent onCalibrationAccepted;
    public UnityEvent onCalibrationReset;
    public UnityEvent onCalibrationCancelled;

    private bool isCalibrating = false;
    private CalibrationStep step = CalibrationStep.None;
    private float defaultForward = 0.01f;
    private float defaultBackward = 0.01f;

    private float initialForward = 0.01f;
    private float initialBackward = 0.01f;

    private void OnEnable()
    {
        leftControllerTrigger.OnValueChanged += UserInput;
        rightControllerTrigger.OnValueChanged += UserInput;
        leftDoublePinch.OnValueChanged += UserInput;
        rightDoublePinch.OnValueChanged += UserInput;
    }

    private void OnDisable()
    {
        leftControllerTrigger.OnValueChanged -= UserInput;
        rightControllerTrigger.OnValueChanged -= UserInput;
        leftDoublePinch.OnValueChanged -= UserInput;
        rightDoublePinch.OnValueChanged -= UserInput;
    }

    public void UserInput(bool value)
    {
        if (!value)
        {
            return;
        }

        switch (step)
        {
            case CalibrationStep.Forward:
                CalibrateForward();
                break;
            case CalibrationStep.Backward:
            CalibrateBackward();
                break;
            default: break;
        }
    }

    public void CalibrateForward()
    {
        if (!isCalibrating)
        {
            return;
        }

        Calibrate();

        step = CalibrationStep.Backward;

        onCalibratedForward.Invoke();
    }

    public void CalibrateBackward()
    {
        if (!isCalibrating)
        {
            return;
        }

        Calibrate();

        step = CalibrationStep.Done;

        onCalibratedBackward.Invoke();
    }

    public void StartCalibrating()
    {
        isCalibrating = true;

        initialForward = backwardDistance.Value;
        initialBackward = forwardDistance.Value;

        initialHeadPosition.Value = headPosition.Value;
        initialHeadForward.Value = headForward.Value;

        step = CalibrationStep.Forward;

        onCalibrationStarted.Invoke();
    }

    public void FinishCalibrating()
    {
        isCalibrating = false;
        step = CalibrationStep.None;
        onCalibrationAccepted.Invoke();
    }

    public void CancelCalibration()
    {
        isCalibrating = false;

        forwardDistance.Value = initialForward;
        backwardDistance.Value = initialBackward;

        step = CalibrationStep.None;

        onCalibrationCancelled.Invoke();
    }

    public void ResetCalibration()
    {
        initialHeadPosition.Value = headPosition.Value;
        initialHeadForward.Value = headForward.Value;

        forwardDistance.Value = initialForward;
        backwardDistance.Value = initialBackward;

        step = CalibrationStep.Forward;

        onCalibrationReset.Invoke();
    }

    public void Calibrate()
    {
        if (!isCalibrating) return;

        Vector3 offset = headPosition.Value - initialHeadPosition.Value;
        currentDistance.Value = Vector3.Dot(initialHeadForward.Value, offset);
        //Debug.Log($"Distance: {distance}. Offset: {offset}, Max {Mathf.Max(headFowardDistance.Value, distance)}");
        forwardDistance.Value = Mathf.Max(forwardDistance.Value, currentDistance.Value);
        backwardDistance.Value = Mathf.Abs(Mathf.Min(-backwardDistance.Value, currentDistance.Value));
    }
}

