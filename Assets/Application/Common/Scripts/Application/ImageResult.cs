using UnityEngine;

[System.Serializable]
public class ImageResult
{
    public Texture2D image;
    public Vector2 position;
    public bool isCorrect;
    public float tries;
    public float time;
}
