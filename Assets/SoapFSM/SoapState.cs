using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "SoapState", menuName = "Soap/FSM/Soap State")]
public class SoapState : ScriptableObject
{
    public List<SoapTransition> transitions;
    [Header("Events")]
    [Tooltip("Invoked when the state is entered, after all transitions are initialize")]
    public UnityEvent onEnter;
    [Tooltip("Invoked when the state is exited, after all transitions are cleaned up")]
    public UnityEvent onExit;
    [Header("Delayed Exit")]
    [Tooltip("If true, the state will wait a frame before exiting")]
    public bool DelayedExit = false;
    [Header("Auto Exit")]
    [Tooltip("If true, the state will automatically exit when a transition is taken")]
    public bool AutoExit = false;
    [Tooltip("")]
    public SoapState AutoExitState = null;
    

    private void Initialize()
    {
        foreach (var transition in transitions)
        {
            transition.Initialize();
        }
    }

    private void CleanUp()
    {
        foreach (var transition in transitions)
        {
            transition.CleanUp();
        }
    }

    public void SetAutoExit(bool value)
    {
        AutoExit = value;
    }

    public void Enter()
    {
        Initialize();
        onEnter.Invoke();
        if (AutoExit)
            Exit();
    }

    public void Exit()
    {
        CleanUp();

        if (DelayedExit)
        {
            FSMCoroutineSingleton.Instance.RunCoroutine(DelayedExitCoroutine());
            return;
        }
        else
        {
            ImmediateExit();
        }
    }

    public IEnumerator DelayedExitCoroutine()
    {
        yield return null;
        onExit.Invoke();
    }

    public void ImmediateExit()
    {
        onExit.Invoke();
    }

    public void SetTransitionTargetState(int transitionId, SoapState targetState)
    {
        if (transitions == null)
        {
            Debug.LogError("Transitions list is null");
            return;
        }

        if (transitionId < 0 || transitionId >= transitions.Count)
        {
            Debug.LogError("Invalid transition id");
            return;
        }

        transitions[transitionId].targetState = targetState;
    }

    public void SetTransition0TargetState(SoapState targetState)
    {
        SetTransitionTargetState(0, targetState);
    }

    public void SetTransition1TargetState(SoapState targetState)
    {
        SetTransitionTargetState(1, targetState);
    }
}
