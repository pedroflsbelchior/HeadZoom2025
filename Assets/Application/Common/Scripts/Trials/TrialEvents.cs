using Obvious.Soap;
using System;
using UnityEngine;
using UnityEngine.Events;

public class TrialEvents : MonoBehaviour
{
    public ScriptableEventInt TrialStart;
    public ScriptableEventInt TrialStop;

    public UnityEvent onTrialStart;
    public UnityEvent onTrialStop;

    private void OnEnable()
    {
        TrialStart.OnRaised += OnTrialStart;
        TrialStop.OnRaised += OnTrialStop;
    }

    private void OnDisable()
    {
        TrialStart.OnRaised -= OnTrialStart;
        TrialStop.OnRaised -= OnTrialStop;
    }

    private void OnTrialStart(int obj)
    {
        onTrialStart.Invoke();
    }

    private void OnTrialStop(int obj)
    {
        onTrialStop.Invoke();
    }
}
