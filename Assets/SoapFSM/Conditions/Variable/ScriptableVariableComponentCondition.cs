using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableVariableCondition", menuName = "Soap/FSM/Conditions/Variables/Component Variable Trigger")]
public class ScriptableVariableComponentCondition : ScriptableVariableTriggerCondition<Component>
{
    public ComponentVariable Variable;
    public override ScriptableVariable<Component> GetVariable() => Variable;
}
