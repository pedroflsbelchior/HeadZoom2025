using Obvious.Soap;
using System;
using System.Reflection;
using UnityEngine;

public class ScriptableEventTriggerCondition<T> : SoapCondition
{
    protected ScriptableEvent<T> scriptableEvent;

    public void OnEventFired(T arg)
    {
        Debug.Log($"Event Trigger Fired on {name}");
        onConditionMet.Invoke();
        AfterConditionMet();
    }

    public override void Initialize()
    {
        if (scriptableEvent == null)
            return;

        scriptableEvent.OnRaised += OnEventFired;
    }

    public override void CleanUp()
    {
        if (scriptableEvent == null)
            return;

        scriptableEvent.OnRaised -= OnEventFired;
    }
}
