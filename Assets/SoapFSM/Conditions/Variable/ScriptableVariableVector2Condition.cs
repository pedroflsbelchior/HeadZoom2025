using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableVariableCondition", menuName = "Soap/FSM/Conditions/Variables/Vector2 Variable Trigger")] 
public class ScriptableVariableVector2Condition : ScriptableVariableTriggerCondition<Vector2> 
{ 
    public Vector2Variable Variable; 
    public override ScriptableVariable<Vector2> GetVariable() => Variable; 
}
