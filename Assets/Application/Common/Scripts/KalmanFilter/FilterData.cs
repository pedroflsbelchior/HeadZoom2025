using Obvious.Soap;
using UnityEngine;

[CreateAssetMenu(fileName = "FilterData", menuName = "HeadZoom/FilterData")]
public class FilterData : ScriptableObject
{
    [SerializeField]
    private AnimationCurve QCurve;
    [SerializeField]
    private AnimationCurve RCurve;

    public virtual (float Q, float R) CalculateParameters(float percentage)
    {
        float value = percentage / 2f + 0.5f;
        return (QCurve.Evaluate(value), RCurve.Evaluate(value));
    }

}

