using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Listens to changes in a ScriptableVariable and invokes a UnityEvent when the variable changes.
/// </summary>
/// <typeparam name="T">The type of the variable.</typeparam>
public class ScriptableVariableListener<T,T1> : MonoBehaviour where T : ScriptableVariable<T1>
{
    /// <summary>
    /// The ScriptableVariable to listen to.
    /// </summary>
    public T variable;

    public bool invokeOnStart = true;

    /// <summary>
    /// The event to invoke when the variable changes.
    /// </summary>
    [Header("Events")]
    public UnityEvent<T1> onValueChanged;

    /// <summary>
    /// Registers the OnVariableChanged method to the variable's OnValueChanged event.
    /// </summary>
    private void OnEnable()
    {
        variable.OnValueChanged += OnVariableChanged;
    }

    /// <summary>
    /// Unregisters the OnVariableChanged method from the variable's OnValueChanged event.
    /// </summary>
    private void OnDisable()
    {
        variable.OnValueChanged -= OnVariableChanged;
    }

    /// <summary>
    /// Invokes the onValueChanged event with the new value of the variable.
    /// </summary>
    /// <param name="t">The new value of the variable.</param>
    private void OnVariableChanged(T1 t)
    {
        onValueChanged.Invoke(t);
    }

    private void Start()
    {
        if (invokeOnStart)
            onValueChanged.Invoke(variable.Value);
    }
}
