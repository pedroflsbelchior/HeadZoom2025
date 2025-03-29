using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;

public class BoolVariableListener : MonoBehaviour
{
    public bool checkOnEnable = false;
    public BoolVariable variable;

    [Header("Events")]
    public UnityEvent<bool> onValueChanged;
    [Header("True/False Events")]
    public UnityEvent onTrue;
    public UnityEvent onFalse;

    private void OnEnable()
    {
        if (checkOnEnable)
            OnValueChanged(variable.Value);
        variable.OnValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        variable.OnValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(bool value)
    {
        onValueChanged.Invoke(value);
        if (value)
            onTrue.Invoke();
        else
            onFalse.Invoke();
    }
}
