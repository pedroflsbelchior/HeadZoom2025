using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableVariableCondition", menuName = "Soap/FSM/Conditions/Variables/String Variable Trigger")]
public class ScriptableVariableStringCondition : ScriptableVariableTriggerCondition<string>
{
    public StringVariable Variable;
    public override ScriptableVariable<string> GetVariable() => Variable;
}
