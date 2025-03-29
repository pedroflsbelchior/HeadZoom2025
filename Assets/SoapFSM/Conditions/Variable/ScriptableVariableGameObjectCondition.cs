using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableVariableCondition", menuName = "Soap/FSM/Conditions/Variables/GameObject Variable Trigger")] 
public class ScriptableVariableGameObjectCondition : ScriptableVariableTriggerCondition<GameObject> 
{ 
    public GameObjectVariable Variable; 
    public override ScriptableVariable<GameObject> GetVariable() => Variable; 
}
