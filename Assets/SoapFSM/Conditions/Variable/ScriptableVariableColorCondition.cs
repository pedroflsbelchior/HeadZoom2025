using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableVariableCondition", menuName = "Soap/FSM/Conditions/Variables/Color Variable Trigger")]
public class ScriptableVariableColorCondition : ScriptableVariableTriggerCondition<Color>
{
    public ColorVariable Variable;
    public override ScriptableVariable<Color> GetVariable() => Variable;
}
