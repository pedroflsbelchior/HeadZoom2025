using UnityEngine;
using UnityEngine.UI;

public class ThumbnailController : MonoBehaviour
{
    public RawImage image;

    public GameObject successVisual = null;
    public GameObject triesVisual = null;
    public GameObject timeVisual = null;

    public void ShowVisual(ImageResult result)
    {
        successVisual.SetActive(result.isCorrect);
        triesVisual.SetActive(result.tries >= 3);
        timeVisual.SetActive(result.time >= 120);
    }

}
