using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;

public class Filename : MonoBehaviour
{
    public IntVariable trialId;
    public IntVariable userId;
    [Header("Events")]
    public UnityEvent<string> newFilename;

    public void OnNewTrial()
    {
        newFilename.Invoke($"User{userId.Value}_Trial{trialId.Value}_{Time.unscaledTimeAsDouble}");
    }
}
