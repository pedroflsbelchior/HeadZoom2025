using Obvious.Soap;
using UnityEngine;

public class ResultsManager : MonoBehaviour
{
    public ScriptableListResult Results;

    public TrialImageDataVariable imageData;
    public FloatVariable timeEllapsed;
    public Vector3Variable targetLocalPosition;
    public IntVariable currentTry;

    public void Success()
    {
        ImageResult imageResult = new ImageResult();
        imageResult.image = imageData.Value.data.image;
        imageResult.time = timeEllapsed;
        imageResult.position = targetLocalPosition.Value;
        imageResult.isCorrect = true;
        imageResult.tries = currentTry.Value;

        Results.Add(imageResult);
    }

    public void AttemptsExhausted()
    {
        ImageResult imageResult = new ImageResult();
        imageResult.image = imageData.Value.data.image;
        imageResult.time = timeEllapsed;
        imageResult.position = targetLocalPosition.Value;
        imageResult.isCorrect = false;
        imageResult.tries = currentTry.Value;

        Results.Add(imageResult);
    }

    public void TimeExpired()
    {
        ImageResult imageResult = new ImageResult();
        imageResult.image = imageData.Value.data.image;
        imageResult.time = timeEllapsed;
        imageResult.position = targetLocalPosition.Value;
        imageResult.isCorrect = false;
        imageResult.tries = currentTry.Value;

        Results.Add(imageResult);
    }
}
