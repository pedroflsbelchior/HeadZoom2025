using Obvious.Soap;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WallyController : MonoBehaviour
{
    //[Header("References")]
    //public Renderer targetRenderer;
    //public AnchorConsumer anchorConsumer;
    [Header("Image")]
    //public StringVariable currentImageName;
    public Vector3Variable targetLocalPosition;

    public Transform UV = null;

    public TrialDataVariable trialData;
    public TrialImageDataVariable currentTrialImageData;
    public List<ImageResult> results = new();

    public IntVariable currentTry;

    public BoolVariable isHovering;
    private bool isTransitioning = false;
    public bool isGrading = false;

    public UnityEvent onBeginSuccessTransition;
    public UnityEvent onBeginFailureTransition;
    public UnityEvent onBeginGrading;
    public UnityEvent onEndTransition;
    public UnityEvent<int> onFinish;

    private bool isTrialRunning = false;
    private int currentImage = -1;
    private int currentTrial = 0;
    private List<TrialImageData> imageDataList = new();

    private void OnEnable()
    {
        trialData.OnValueChanged += SetTrialData;
    }

    private void OnDisable()
    {
        trialData.OnValueChanged -= SetTrialData;
    }

    private bool SelectedWally()
    {
        return isHovering.Value;
    }

    private void SaveResult()
    {
        var result = new ImageResult();
        result.position = targetLocalPosition.Value;
        result.image = currentTrialImageData.Value.data.image;
        results.Add(result);
        Debug.Log("Added result: " + result.position + " | " + result.image.name);
        currentTry.Value = 0;
    }

    public void OnLeftPinch(bool obj)
    {
        ProcessPinch(obj);
    }

    public void OnRightPinch(bool obj)
    {
        ProcessPinch(obj);
    }

    private void ProcessPinch(bool obj)
    {
        if (!obj || !isTrialRunning || isGrading)
            return;

        ProcessTry();
    }

    private void ProcessTry()
    {
        if (SelectedWally())
        {
            SaveResult();
            if (!isTransitioning)
                StartCoroutine(SuccessTransition());
        }
        else
        {
            currentTry.Value++;

            bool usedAllAttempts = trialData.Value.triesPerImage > 0 && currentTry.Value >= trialData.Value.triesPerImage;
            bool noMoreAttempts = usedAllAttempts || timeExpired;
            
            if (noMoreAttempts)
                SaveResult();

            if (!isTransitioning)
                StartCoroutine(
                    FailTransition(noMoreAttempts));
        }
    }

    public void ResetTrial(int obj)
    {
        isTrialRunning = false;
        currentImage = -1;
    }


    public void SetTrialData(TrialData data)
    {
        if (data == null || isTrialRunning)
            return;

        imageDataList = trialData.Value.GetRandomizedImageData();
    }

    public void InitializeTrial(int obj)
    {
        Debug.Log("Initializing Trial: " + obj + ", with trial " + trialData.Value.name);

        results.Clear();
        isTrialRunning = true;
        currentTrial = obj;
        
        if (imageDataList == null || imageDataList.Count == 0)
            imageDataList = trialData.Value.GetRandomizedImageData();

        currentImage = -1;

        NextImage();
    }

    private Texture2D currentTexture = null;

    private Coroutine timeForImage = null;

    private bool timeExpired = false;

    IEnumerator TimeImage(int seconds) {
        timeExpired = false;
        yield return new WaitForSeconds(seconds);
        timeExpired = true;
        ProcessTry();
    }

    public void NextImage()
    {
        currentImage++;

        if (currentImage >= imageDataList.Count)
        {
            Debug.Log($"Failing out because currentImage {currentImage} >= imageDataList.Count {imageDataList.Count}");

            currentTexture = null;
            currentTry.Value = 0;
            onFinish.Invoke(currentTrial);
            return;
        }

        currentTrialImageData.Value = imageDataList[currentImage];
        var imageData = imageDataList[currentImage];

        //anchorConsumer.SetAnchor(imageData.anchor);

        //currentTexture = imageData.data.image;
        //currentImageName.Value = currentTexture.name;
        //targetRenderer.material.mainTexture = currentTexture;

        //targetRenderer.transform.localScale = new Vector3((float)currentTexture.width / (float)currentTexture.height, 1, 1);
        
        if (timeForImage != null)
            StopCoroutine(timeForImage);

        if (trialData.Value.secondsPerImage > 0)
            timeForImage = StartCoroutine(TimeImage(trialData.Value.secondsPerImage));

        UV.localPosition = new Vector3(imageData.data.hitTarget.x, imageData.data.hitTarget.y, -.01f);

        //Debug.Log($"Texture: {currentTexture.name} | {currentTexture.width}x{currentTexture.height} | {(float)currentTexture.width / (float)currentTexture.height}");
    }

    public void StartGrading()
    {
        isGrading = true;
        onBeginGrading.Invoke();
    }

    public void StopGrading()
    {
        isGrading = false;
        NextImage();
    }

    IEnumerator FailTransition(bool moveToNextImage = false)
    {
        isTransitioning = true;
        onBeginFailureTransition.Invoke();
        yield return new WaitForSeconds(1);
        onEndTransition.Invoke();
        isTransitioning = false;
        if (moveToNextImage)
        {
            Debug.Log("Moving to next image");
            StartGrading();
        }
    }

    IEnumerator SuccessTransition()
    {
        isTransitioning = true;
        onBeginSuccessTransition.Invoke();
        yield return new WaitForSeconds(1);
        onEndTransition.Invoke();
        isTransitioning = false;
        StartGrading();
    }


    private void OnDestroy()
    {
        trialData.Value = null;
    }
}
