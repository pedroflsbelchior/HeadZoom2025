using UnityEngine;
using UnityEngine.Events;

public class TrialRecordingChecks : MonoBehaviour
{
    [SerializeField] private TrialDataVariable trialData;
    
    [SerializeField] private UnityEvent onEnableRecordingData;
    [SerializeField] private UnityEvent onDisableRecordingData;
    [SerializeField] private UnityEvent onEnableRecordingVideo;
    [SerializeField] private UnityEvent onDisableRecordingVideo;

    public void StartTrial(int trialId) => StartTrial();
    public void StartTrial()
    {
        if (trialData.Value == null)
        {
            Debug.LogError("TrialData is null");
            return;
        }

        if (trialData.Value.RecordData)
        {
            onEnableRecordingData.Invoke();
        }

        if (trialData.Value.RecordVideo)
        {
            onEnableRecordingVideo.Invoke();
        }
    }

    public void StopTrial(int trialId) => StopTrial();
    public void StopTrial()
    {
        if (trialData.Value == null)
        {
            Debug.LogError("TrialData is null");
            return;
        }

        if (trialData.Value.RecordData)
        {
            onDisableRecordingData.Invoke();
        }

        if (trialData.Value.RecordVideo)
        {
            onDisableRecordingVideo.Invoke();
        }
    }

}
