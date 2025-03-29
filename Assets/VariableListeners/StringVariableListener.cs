using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;

public class StringVariableListener : MonoBehaviour
{
    public StringVariable variable;
    public UnityEvent<string> onValueChange;

    private void OnEnable()
    {
        onValueChange.Invoke(variable.Value);
        variable.OnValueChanged += onValueChange.Invoke;
    }

    private void OnDisable()
    {
        variable.OnValueChanged -= onValueChange.Invoke;
    }
}
