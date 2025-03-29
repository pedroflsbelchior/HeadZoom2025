using Obvious.Soap;
using System;
using UnityEngine;
using UnityEngine.Events;

public class IntVariableListener : MonoBehaviour
{
    public IntVariable variable;

    [Header("Events")]
    public UnityEvent<int> onValueChanged;
    public UnityEvent<string> onValueStringChanged;

    private void OnEnable()
    {
        if (variable == null)
        {
            Debug.LogError("Variable is not set in " + name);
            return;
        }
        OnValueChanged(variable.Value);
        variable.OnValueChanged += OnValueChanged;
    }

    private void OnDisable()
    {
        if (variable == null)
        {
            return;
        }
        variable.OnValueChanged -= OnValueChanged;
    }

    private void OnValueChanged(int obj)
    {
        onValueChanged.Invoke(obj);
        onValueStringChanged.Invoke(obj.ToString());
    }


}
