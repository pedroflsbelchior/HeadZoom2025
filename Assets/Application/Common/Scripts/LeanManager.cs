using Obvious.Soap;
using System;
using UnityEngine;

public class LeanManager : MonoBehaviour
{
    [Header("Head Tracking")]
    public Vector3Variable headPosition;
    public Vector3Variable headDefaultPosition;
    public Vector3Variable headDefaultForward;

    [Header("Head Distance")]
    public FloatVariable headForwardDistance;
    public FloatVariable headBackwardDistance;
    public FloatVariable headCurrentDistance;

    [Header("Events")]
    public ScriptableEventInt TrialStart;
    public ScriptableEventInt TrialStop;

    private void OnEnable()
    {
        headPosition.OnValueChanged += OnHeadPositionChanged;
        TrialStart.OnRaised += OnTrialStart;
        TrialStop.OnRaised += OnTrialStop;
    }

    private void OnDisable()
    {
        headPosition.OnValueChanged -= OnHeadPositionChanged;
        TrialStart.OnRaised -= OnTrialStart;
        TrialStop.OnRaised -= OnTrialStop;
    }

    private bool isTrialRunning = false;

    private void OnTrialStart(int obj)
    {
        isTrialRunning = true;
    }

    private void OnTrialStop(int obj)
    {
        isTrialRunning = false;
    }

    private void OnHeadPositionChanged(Vector3 vector)
    {
        if (!isTrialRunning)
        {
            return;
        }

        Vector3 offset = headPosition.Value - headDefaultPosition.Value;
        float distance = Vector3.Dot(offset, headDefaultForward.Value);
        headCurrentDistance.Value = distance;
    }
}
