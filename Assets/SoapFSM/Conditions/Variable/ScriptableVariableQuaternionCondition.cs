using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableVariableCondition", menuName = "Soap/FSM/Conditions/Variables/Quaternion Variable Trigger")]
public class ScriptableVariableQuaternionCondition : ScriptableVariableTriggerCondition<Quaternion>
{
    public QuaternionVariable Variable;
    public override ScriptableVariable<Quaternion> GetVariable() => Variable;
}
