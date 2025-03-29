using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableVariableCondition", menuName = "Soap/FSM/Conditions/Variables/Vector3 Variable Trigger")] 
public class ScriptableVariableVector3Condition : ScriptableVariableTriggerCondition<Vector3> 
{ 
    public Vector3Variable Variable; 
    public override ScriptableVariable<Vector3> GetVariable() => Variable;
}
