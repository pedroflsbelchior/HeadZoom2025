using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableVariableCondition", menuName = "Soap/FSM/Conditions/Variables/Bool Variable Trigger")]
public class ScriptableVariableBoolCondition : ScriptableVariableTriggerCondition<bool>
{
    public BoolVariable Variable;
    public override ScriptableVariable<bool> GetVariable() => Variable;
}
