using Obvious.Soap;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AttemptManager : MonoBehaviour
{
    public TrialDataVariable trialData;
    public TrialImageDataVariable trialImageData;
    public Vector3Variable targetLocalPosition;
    public BoolVariable isHovering;
    public IntVariable currentTry;
    [Header("Events")]
    public UnityEvent onSuccessfulAttempt;
    public UnityEvent onFailedAttempt;
    public UnityEvent onLastFailedAttempt;
    public UnityEvent onAttemptsExhausted;

    private bool isWaitingForAttempt = false;

    public void StartWaitingForAttempt() => isWaitingForAttempt = true;
    public void StopWaitingForAttempt() => isWaitingForAttempt = false;

    public void OnEnable()
    {
        trialImageData.OnValueChanged += SetCurrentTrialImageData;
    }

    public void OnDisable()
    {
        trialImageData.OnValueChanged -= SetCurrentTrialImageData;
    }

    private void SetCurrentTrialImageData(TrialImageData data)
    {
        currentTry.Value = 0;
    }

    public void OnLeftPinch(bool obj)
    {
        if (obj) Attempt();
    }

    public void OnRightPinch(bool obj)
    {
        if (obj) Attempt();
    }

    private void Attempt()
    {
        if (!isWaitingForAttempt)
            return;
        if (trialData == null || trialData.Value == null)
            return;
        if (trialImageData == null || trialImageData.Value == null)
            return;


        currentTry.Value++;


        if (isHovering.Value)
        {
            onSuccessfulAttempt.Invoke();
        }
        else
        {
            if (trialData.Value.triesPerImage > 0 && currentTry.Value >= trialData.Value.triesPerImage)
            {
                onLastFailedAttempt.Invoke();
                onAttemptsExhausted.Invoke();
            }
            else
                onFailedAttempt.Invoke();
        }
    }



    //public BoolVariable isHovering;
    //public TrialDataVariable trialData;
    //public TrialImageDataVariable currentTrialImageData;
    //public IntVariable currentTry;

    //public UnityEvent onTimeOut;
    //public UnityEvent onAttemptFail;
    //public UnityEvent onAttemptSuccess;
    //public UnityEvent onAttemptsFinished;

    //private Coroutine timeImageCoroutine;
    //private bool hasTimeRunOut = false;
    //private bool isAttempting = false;

    //public void OnEnable()
    //{
    //    currentTrialImageData.OnValueChanged += SetCurrentTrialImageData;
    //}

    //public void OnDisable()
    //{
    //    currentTrialImageData.OnValueChanged -= SetCurrentTrialImageData;
    //}

    //IEnumerator TimeImage(int seconds)
    //{
    //    hasTimeRunOut = false;
    //    yield return new WaitForSeconds(seconds);
    //    hasTimeRunOut = true;
    //    CheckAttemptLimits();
    //}


    //private void SetCurrentTrialImageData(TrialImageData trialImageData)
    //{
    //    if (timeImageCoroutine != null)
    //        StopCoroutine(timeImageCoroutine);
    //    timeImageCoroutine = StartCoroutine(TimeImage(trialData.Value.secondsPerImage));
    //}


    //private void SaveResult()
    //{
    //    trialData.Value.AddAttempt(targetLocalPosition.Value);

    //    if (currentTry != null)
    //        currentTry.Value = 0;
    //}

    //public void OnLeftPinch(bool obj)
    //{
    //    ProcessPinch(obj);
    //}

    //public void OnRightPinch(bool obj)
    //{
    //    ProcessPinch(obj);
    //}

    //private void ProcessPinch(bool obj)
    //{
    //    if (!obj || !trialData.Value.isRunning)
    //        return;

    //    ProcessAttempt();
    //}

    //private void CheckAttemptLimits()
    //{
    //    bool attemptsExhausted = trialData.Value.triesPerImage > 0 && currentTry.Value >= trialData.Value.triesPerImage;
    //    bool timeExpired = trialData.Value.secondsPerImage > 0 && hasTimeRunOut;

    //    if (attemptsExhausted || timeExpired)
    //    {
    //        onAttemptsFinished.Invoke();
    //        return;
    //    }
    //}

    //private void ProcessAttempt()
    //{
    //    if (isHovering.Value)
    //    {
    //        SaveResult();
    //        if (!isTransitioning)
    //            StartCoroutine(SuccessTransition());
    //    }
    //    else
    //    {
    //        currentTry.Value++;

    //        bool usedAllAttempts = trialData.Value.triesPerImage > 0 && currentTry.Value >= trialData.Value.triesPerImage;
    //        bool noMoreAttempts = usedAllAttempts || timeExpired;

    //        if (noMoreAttempts)
    //            SaveResult();

    //        if (!isTransitioning)
    //            StartCoroutine(
    //                FailTransition(noMoreAttempts));
    //    }
    //}
}
