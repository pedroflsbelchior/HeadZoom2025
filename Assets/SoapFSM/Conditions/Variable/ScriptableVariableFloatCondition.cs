using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableVariableCondition", menuName = "Soap/FSM/Conditions/Variables/Float Variable Trigger")] 
public class ScriptableVariableFloatCondition : ScriptableVariableTriggerCondition<float>
{
    public FloatVariable Variable;
    public override ScriptableVariable<float> GetVariable() => Variable;
}