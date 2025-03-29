using Obvious.Soap;
using UnityEngine;

public class ResetTrial : MonoBehaviour
{
    public FloatVariable forwardDistance;
    public FloatVariable backwardDistance;
    public Vector3Variable headDefaultPosition;
    public Vector3Variable headDefaultForward;
    public ScriptableListResult results;

    public void ResetAll()
    {
        forwardDistance.Value = 0.01f;
        backwardDistance.Value = 0.01f;
        headDefaultPosition.Value = Vector3.zero;
        headDefaultForward.Value = Vector3.forward;
        results.Clear();
    }
}
