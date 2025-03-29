using Obvious.Soap;
using System.Collections;
using UnityEngine;

public class TrialManager : MonoBehaviour
{
    public IntVariable CurrentTrialID;
    public ScriptableEventInt StartTrialEvent;
    public ScriptableEventInt EndTrialEvent;

    private int currentTrialID = 0;

    public int GetTrialIDFromPlayerPrefs()
    {
        return PlayerPrefs.GetInt("TrialID", 0);
    }

    public void SetTrialIDInPlayerPrefs(int trialID)
    {
        PlayerPrefs.SetInt("TrialID", trialID);
    }

    public void StartTrial()
    {

        currentTrialID = GetTrialIDFromPlayerPrefs();
        currentTrialID++;
        SetTrialIDInPlayerPrefs(currentTrialID);

        CurrentTrialID.Value = currentTrialID;

        Debug.Log($"Starting trial {CurrentTrialID.Value}");
        StartTrialEvent.Raise(currentTrialID);
    }

    public void EndTrial()
    {
        EndTrialEvent.Raise(currentTrialID);
    }

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
    }
}
