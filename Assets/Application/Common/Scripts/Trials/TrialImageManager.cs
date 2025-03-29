using NUnit.Framework;
using Obvious.Soap;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TrialImageManager : MonoBehaviour
{
    public TrialDataVariable trialData;
    public IntVariable currentImageIndex;
    public IntVariable totalImages;
    [Header("Events")]
    public UnityEvent<TrialImageData> onImageChanged;
    public UnityEvent onImagesExhausted;
    [Header("Grading Events")]
    public UnityEvent onRequestGrading;
    public UnityEvent onSkipGrading;
    [Header("Questionnaire Events")]
    public UnityEvent onPauseForQuestionnaire;
    public UnityEvent onContinueWithoutQuestionnaire;

    private int currentImage = -1;
    private List<TrialImageData> images;

    public void ABC(string title)
    {
       Debug.Log($"IMAGE CHANGED TO {title}");
    }

    public void OnEnable()
    {
        trialData.OnValueChanged += SetTrialData;
    }

    public void OnDisable()
    {
        trialData.OnValueChanged -= SetTrialData;
    }

    private void SetTrialData(TrialData data)
    {
        currentImage = -1;
        if (data != null)
            images = data == null 
                ? new List<TrialImageData>() 
                : data.GetRandomizedImageData();
        currentImageIndex.Value = 0;
        totalImages.Value = images.Count;
    }

    public void ChangeImage()
    {
        currentImage++;
        if (currentImage >= images.Count)
            onImagesExhausted.Invoke();
        else
        {
            currentImageIndex.Value = currentImage;
            onImageChanged.Invoke(images[currentImage]);
        }
    }

    public void CheckIfRequestGrading()
    {
        if (ShouldRequestGrading())
            onRequestGrading.Invoke();
        else
            onSkipGrading.Invoke();
    }

    private bool ShouldRequestGrading()
    {
        return trialData.Value.RequestGrading;
    }

    public void CheckIfPauseForQuestionnaire()
    {
        if (ShouldPauseForQuestionnaire())
            onPauseForQuestionnaire.Invoke();
        else
            onContinueWithoutQuestionnaire.Invoke();
    }

    // Called before updating currentImage
    private bool ShouldPauseForQuestionnaire()
    {
        // If the trial does not call for pausing for a questionnaire, return false
        if (!trialData.Value.PauseForQuestionnaire)
            return false;

        // If we are at the first image, or the trial does not change anchors, return false
        if (currentImage < 0 || trialData.Value.imagesPerAnchor <= 0)
            return false;

        // If we are at the last image, return false
        if (currentImage + 1 >= images.Count)
            return false;

        // If the current image is the last image of the current anchor, return true
        return (currentImage + 1) % trialData.Value.imagesPerAnchor == 0;
    }
}
