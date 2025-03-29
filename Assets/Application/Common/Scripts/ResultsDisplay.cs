using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultsDisplay : MonoBehaviour
{
    public List<ThumbnailController> thumbnails = new();
    public ScriptableListResult results;

    public void ShowResult(ImageResult result, RawImage image)
    {
        float h = 1f;
        float ph = 1.0f / result.image.height;
        float ch = 0.12f / h;
        float y = 0.06f / h;

        float w = (float)result.image.width / result.image.height;
        float pw = 1.0f / result.image.width;
        float cw = 0.12f / w;
        float x = 0.06f / w;

        image.texture = result.image;
        image.uvRect = new Rect(result.position.x + 0.5f - x, result.position.y + 0.5f - y, cw, ch);


    }

    private void OnEnable()
    {

        for (int i = 0; i < results.Count && i < thumbnails.Count; i++)
        {
            thumbnails[i].gameObject.SetActive(true);
            ShowResult(results[i], thumbnails[i].image);
            thumbnails[i].ShowVisual(results[i]);
        }

        if (results.Count < thumbnails.Count)
        {
            for (int i = results.Count; i < thumbnails.Count; i++)
            {
                thumbnails[i].gameObject.SetActive(false);
            }
        }
    }
}
