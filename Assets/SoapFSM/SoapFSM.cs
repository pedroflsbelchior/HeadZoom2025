using UnityEngine;

public class SoapFSM : MonoBehaviour
{
    public SoapState currentState = null;

    public void Start()
    {
        if (currentState != null)
        {
            HookTransitions();
            currentState.Enter();
        }
    }

    public void OnEnable()
    {
        
    }

    public void OnDisable()
    {
        
    }

    public void SetState(SoapState state)
    {
        Debug.Log($"Setting state to {state.name}");

        UnhookTransitions();
        currentState.Exit();

        currentState = state;

        HookTransitions();
        currentState.Enter();
    }

    private void AutoExit()
    {
        if (currentState.AutoExit)
            SetState(currentState.AutoExitState);
    }

    private void HookTransitions()
    {
        Debug.Log("Hooking transitions");
        if (currentState == null) return;
        Debug.Log("currentState is not null");
        foreach (SoapTransition transition in currentState.transitions)
            transition.onTransitionConditionMet.AddListener(SetState);

        if (currentState.AutoExit)
            currentState.onExit.AddListener(AutoExit);
    }

    private void UnhookTransitions()
    {
        Debug.Log("Unhooking transitions");
        if (currentState == null) return;
        Debug.Log("currentState is not null");

        foreach (var transition in currentState.transitions)
            transition.onTransitionConditionMet.RemoveListener(SetState);

        if (currentState.AutoExit)
            currentState.onExit.RemoveListener(AutoExit);
    }
}
