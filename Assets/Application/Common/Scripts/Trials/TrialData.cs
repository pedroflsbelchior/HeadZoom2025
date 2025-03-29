using System;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public static class ThreadSafeRandom
{
    [ThreadStatic] private static System.Random Local;

    public static System.Random ThisThreadsRandom
    {
        get { return Local ?? (Local = new System.Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
    }
}

static class MyExtensions
{
    public static IList<T> Shuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
        return list;
    }
}

public enum HeadZoomMode
{
    Static,
    Parallel,
    Tilt
}

[System.Serializable]
public class TrialImageData
{
    public AnchorObject anchor;
    public ImageDifficulty difficulty;
    public TrialImage data;
}

[CreateAssetMenu(fileName = "TrialData", menuName = "HeadZoom/TrialData")]
public class TrialData : ScriptableObject
{
    public List<TrialImage> _images = new();
    public List<AnchorObject> _anchors = new();
    [Header("Settings")]
    public int imagesPerAnchor = 2;
    public int secondsPerImage = 0;
    public int triesPerImage = 3;
    public bool RandomizeAnchorOrder = true;
    public bool RandomizeImageOrder = true;
    public bool RequestGrading = false;
    public bool PauseForQuestionnaire = false;
    [Tooltip("Minimum distance between a click and Wally, in the image's local space (normalized 0 to 1)")]
    public float hitRadius = 0.06f;
    [Header("Recording")]
    [SerializeField] private bool _recordData = true;
    [SerializeField] private bool _recordVideo = false;

    public bool RecordData { get => _recordData; }
    public bool RecordVideo { get => _recordVideo; }

    [Header("For Debugging, read only")]
    [SerializeField] private bool isRunning = false;

    public void StartTrial()
    {
        isRunning = true;
    }

    public void StopTrial()
    {
        isRunning = false;
    }

    private List<Vector2> attempts = new List<Vector2>();

    public void AddAttempt(Vector2 attempt)
    {
        attempts.Add(attempt);
    }

    public void ClearAttempts()
    {
        attempts.Clear();
    }

    public List<Vector2> GetAttempts()
    {
        return attempts;
    }

    public List<TrialImageData> GetRandomizedImageData()
    {
        List<TrialImageData> dataList = new();
        var anchors = new List<AnchorObject>(_anchors);
        if (RandomizeAnchorOrder)
            anchors.Shuffle();

        var images = new List<TrialImage>(_images);
        if (RandomizeImageOrder)
            images.Shuffle();

        if (anchors == null || anchors.Count == 0)
        {
            Debug.Log("No anchors found in TrialData");
            return dataList; 
        }

        for (var i = 0; i < images.Count; i++)
        {
            TrialImageData data = new TrialImageData();
            data.anchor = anchors[Mathf.FloorToInt((float)i/imagesPerAnchor) % anchors.Count];
            data.difficulty = images[i].difficulty;
            data.data = images[i];
            dataList.Add(data);
        }

        return dataList;
    }
}
