using UnityEngine;
using Obvious.Soap;
using System;

public class MoveToHeadHeight : MonoBehaviour
{
    public bool alwaysMove = false;
    public ScriptableEventInt TrialStart;
    public Vector3Variable headPosition;
    public Vector3 offset;

    private void OnEnable()
    {
        TrialStart.OnRaised += OnTrialStart;
    }

    private void OnTrialStart(int obj)
    {
        SetHeight();
    }

    public void SetHeight()
    {
        transform.position = transform.position.SetY(headPosition.Value.y + offset.y);
    }

    private void Update()
    {
        if (alwaysMove)
        {
            SetHeight();
        }
    }
}
