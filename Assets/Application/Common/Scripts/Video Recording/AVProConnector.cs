// THIS FILE IS A CONNECTOR FOR AVPRO MOVIE CAPTURE.
// IT IS NOT USED IN THE CURRENT VERSION OF THE PROJECT.

//using RenderHeads.Media.AVProMovieCapture;
using UnityEngine;
using UnityEngine.Events;

public class AVProConnector : MonoBehaviour
{
    //public CaptureFromCamera captureFromCamera = null;
    [SerializeField] private TrialDataVariable trialData = null;

    public UnityEvent onStartRecording;

    private bool trialIsRunning = false;

    public void StartTrial()
    {
        if (trialIsRunning)
        {
            Debug.Log("AVProConnector (StartTrial): Trial is already running");
            return;
        }

        trialIsRunning = true;
        StartRecording();
    }

    public void StopTrial()
    {
        if (!trialIsRunning)
        {
            Debug.Log("AVProConnector (StopTrial): Trial is not running");
            return;
        }

        trialIsRunning = false;
        StopRecording();
    }

    public void StartRecording()
    {
        //if (captureFromCamera == null)
        //{
        //    Debug.Log("AVProConnector (StartRecording): CaptureFromCamera is null");
        //    return;
        //}

        if (!trialIsRunning)
        {
            Debug.Log("AVProConnector (StartRecording): Trial is not running");
            return;
        }

        if (trialData.Value == null)
        {
            Debug.Log("AVProConnector (StartRecording): TrialData is null");
            return;
        }


        if (!trialData.Value.RecordVideo)
        {
            Debug.Log("AVProConnector (StartRecording): This trial does not record video");
            return;
        }

        //captureFromCamera.StartCapture();
        onStartRecording.Invoke();
    }

    public void PauseRecording()
    {
        //if (captureFromCamera == null)
        //{
        //    Debug.Log("AVProConnector (PauseRecording): CaptureFromCamera is null");
        //    return;
        //}
        
        if (!trialIsRunning)
        {
            Debug.Log("AVProConnector (PauseRecording): Trial is not running");
            return;
        }

        //if (!captureFromCamera.IsPaused())
        //    captureFromCamera.PauseCapture();
    }

    public void ResumeRecording()
    {
        //if (captureFromCamera == null)
        //{
        //    Debug.Log("AVProConnector (ResumeRecording): CaptureFromCamera is null");
        //    return;
        //}
        
        if (!trialIsRunning)
        {
            Debug.Log("AVProConnector (ResumeRecording): Trial is not running");
            return;
        }

        //if (captureFromCamera.IsPaused())
        //    captureFromCamera.ResumeCapture();
    }

    public void StopRecording()
    {
        //if (captureFromCamera == null)
        //{
        //    Debug.Log("AVProConnector (StopRecording): CaptureFromCamera is null");
        //    return;
        //}
        
        //if (!trialIsRunning)
        //{
        //    Debug.Log("AVProConnector (StopRecording): Trial is not running");
        //    return;
        //}

        //captureFromCamera.StopCapture();
    }
}
