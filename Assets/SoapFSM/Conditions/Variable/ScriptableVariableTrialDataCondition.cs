using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableVariableCondition", menuName = "Soap/FSM/Conditions/Variables/TrialData Variable Trigger")] 
public class ScriptableVariableTrialDataCondition : ScriptableVariableTriggerCondition<TrialData> 
{ 
    public TrialDataVariable Variable;
    public override ScriptableVariable<TrialData> GetVariable() => Variable;
}
