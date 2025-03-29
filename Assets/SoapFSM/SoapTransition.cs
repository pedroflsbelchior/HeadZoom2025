using System;
using UnityEngine;
using UnityEngine.Events;

//[CreateAssetMenu(fileName = "SoapTransition", menuName = "Soap/FSM/Soap Transition")]
[System.Serializable]
public class SoapTransition// : ScriptableObject
{
    public string title;
    public SoapState targetState;
    public SoapCondition condition;

    public UnityEvent<SoapState> onTransitionConditionMet;

    public void AddListener(UnityAction<SoapState> listener)
    {
        //Debug.Log("Adding listener");
        onTransitionConditionMet.AddListener(listener);
    }

    public void RemoveListener(UnityAction<SoapState> listener)
    {
        //Debug.Log("Removing listener");
        onTransitionConditionMet.RemoveListener(listener);
    }

    private void OnConditionMet()
    {
        //Debug.Log("Condition met: " + title);
        onTransitionConditionMet.Invoke(targetState);
    }

    public void Initialize()
    {
        //Debug.Log($"Initializing {title}");
        condition.Initialize();
        condition.AddAfterConditionMetListener(OnConditionMet);
    }

    public void CleanUp()
    {
        //Debug.Log($"Cleaning up {title}");
        condition.RemoveAfterConditionMetListener(OnConditionMet);
        condition.CleanUp();
    }
}
