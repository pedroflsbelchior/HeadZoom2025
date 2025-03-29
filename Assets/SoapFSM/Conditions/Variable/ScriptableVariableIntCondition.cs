using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableVariableCondition", menuName = "Soap/FSM/Conditions/Variables/Int Variable Trigger")] 
public class ScriptableVariableIntCondition : ScriptableVariableTriggerCondition<int> 
{ 
    public IntVariable Variable; 
    public override ScriptableVariable<int> GetVariable() => Variable; 
}
