using Obvious.Soap;
using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SoapCondition", menuName = "Soap/FSM/Soap Condition")]
public class SoapCondition : ScriptableObject
{
    public UnityEvent onConditionMet;
    public UnityEvent OnAfterConditionMet;

    public void AddAfterConditionMetListener(UnityAction action)
    {
        Debug.Log($"Adding AfterConditionMet listener to {name}");
        OnAfterConditionMet.AddListener(action);
    }

    public void RemoveAfterConditionMetListener(UnityAction action)
    {
        Debug.Log($"Removing AfterConditionMet listener from {name}");
        OnAfterConditionMet.RemoveListener(action);
    }

    public void AfterConditionMet()
    {
        OnAfterConditionMet?.Invoke();
    }

    public virtual void Initialize()
    {
        Debug.Log($"Calling the base class Initialize method on {name}");
    }

    public virtual void CleanUp()
    {
        Debug.Log($"Calling the base class CleanUp method on {name}");
    }
}
