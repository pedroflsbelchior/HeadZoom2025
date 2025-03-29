using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DifficultyTextures", menuName = "HeadZoom/DifficultyTextures", order = 1)]
public class DifficultyTextures : ScriptableObject
{
    public string name;

    private int currentImage = -1;

    public void ResetCounter() => currentImage = -1;

    public Texture2D GetNextTexture()
    {
        currentImage++;

        if (currentImage >= images.Count)
            return null;
        return images[currentImage];
    }

    public List<Texture2D> images = new();
}
