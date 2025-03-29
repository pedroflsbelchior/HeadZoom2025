using Obvious.Soap;
using UnityEngine;  

public class ScriptableVariableTriggerCondition<T> : SoapCondition
{
    private ScriptableVariable<T> scriptableVariable;

    public virtual ScriptableVariable<T> GetVariable() => scriptableVariable;

    public void OnEventFired(T arg)
    {
        Debug.Log($"Event Trigger Fired on {name}");
        onConditionMet.Invoke();
        AfterConditionMet();
    }

    public override void Initialize()
    {
        if (GetVariable() == null)
            return;

        GetVariable().OnValueChanged += OnEventFired;
        Debug.Log($"Initializing {name} with {GetVariable().name}");
    }

    public override void CleanUp()
    {
        if (GetVariable() == null)
            return;

        GetVariable().OnValueChanged -= OnEventFired;
    }
}
