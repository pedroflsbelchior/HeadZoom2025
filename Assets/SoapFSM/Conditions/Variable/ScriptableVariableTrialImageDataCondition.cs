using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "ScriptableVariableCondition", menuName = "Soap/FSM/Conditions/Variables/TrialImageData Variable Trigger")] 
public class ScriptableVariableTrialImageDataCondition : ScriptableVariableTriggerCondition<TrialImageData> 
{
    [SerializeField]
    protected TrialImageDataVariable Variable;

    public override ScriptableVariable<TrialImageData> GetVariable()
        => Variable;
}
