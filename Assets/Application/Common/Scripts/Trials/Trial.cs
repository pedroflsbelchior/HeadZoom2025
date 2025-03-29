using Obvious.Soap;
using UnityEngine;

public class Trial : MonoBehaviour
{
    public ScriptableEventInt StartTrialEvent;

    public Vector3Variable headPosition;
    public Vector3Variable headForward;

    public Vector3Variable headDefaultPosition;
    public Vector3Variable headDefaultForward;

    private void OnEnable()
    {
        StartTrialEvent.OnRaised += StartTrial;
    }

    private void OnDisable()
    {
        StartTrialEvent.OnRaised -= StartTrial;
    }

    public void StartTrial(int trialID)
    {
        headDefaultPosition.Value = headPosition.Value;
        headDefaultForward.Value = headForward.Value.WithY(0).normalized;
    }
}
