using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableVariableCondition", menuName = "Soap/FSM/Conditions/Variables/Vector2Int Variable Trigger")] 
public class ScriptableVariableVector2IntCondition : ScriptableVariableTriggerCondition<Vector2Int> 
{ 
    public Vector2IntVariable Variable; 
    public override ScriptableVariable<Vector2Int> GetVariable() => Variable;
}
