using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableEventCondition", menuName = "Soap/FSM/Conditions/Events/No Params Event Trigger")]
public class ScriptableEventNoParamTriggerCondition : SoapCondition
{
    [Tooltip("Event that will trigger the transition.")]
    public ScriptableEventNoParam scriptableEvent;

    public void OnEventFired()
    {
        Debug.Log($"Event Trigger Fired on {name}");
        onConditionMet.Invoke();
        AfterConditionMet();
    }

    public void OnEventFired<T>(T arg)
    {
        Debug.Log($"Event Trigger Fired on {name}");
        onConditionMet.Invoke();
        AfterConditionMet();
    }

    public override void Initialize()
    {
        
        if (scriptableEvent == null)
        {
            Debug.Log($"Initialize error: ScriptableEvent is null!");
            return;
        }
        Debug.Log($"Initializing Scriptable Event Condition on {scriptableEvent.name}");

        scriptableEvent.OnRaised += OnEventFired;
    }

    public override void CleanUp()
    {
        if (scriptableEvent == null)
        {
            Debug.Log($"CleanUp error: ScriptableEvent is null!");
            return;
        }
        scriptableEvent.OnRaised -= OnEventFired;
    }
}
