using UnityEngine;
using Obvious.Soap;

[CreateAssetMenu(fileName = "scriptable_variable_" + nameof(TrialData), menuName = "Soap/ScriptableVariables/"+ nameof(TrialData))]
public class TrialDataVariable : ScriptableVariable<TrialData>
{
    public void StartTrial()
    {
        if (Value != null)
            Value.StartTrial();
    }

    public void StopTrial()
    {
        if (Value != null)
            Value.StopTrial();
    }
}

