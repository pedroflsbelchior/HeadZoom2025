using Obvious.Soap;
using UnityEngine;

public class TrialImageDataProvider : MonoBehaviour
{
    public TrialImageDataVariable currentTrialImageData;
    public StringVariable currentImageName;
    public Vector2IntVariable currentImageSize;
    public Vector2Variable hitTarget;


    private void OnEnable()
    {
        currentTrialImageData.OnValueChanged += OnTrialImageDataChanged;
    }

    private void OnDisable()
    {
        currentTrialImageData.OnValueChanged -= OnTrialImageDataChanged;
    }

    private void OnTrialImageDataChanged(TrialImageData trialImageData)
    {
        if (trialImageData == null || trialImageData.data == null)
        {
            currentImageName.Value = "";
            currentImageSize.Value = Vector2Int.zero;
            hitTarget.Value = Vector2.zero;
            return;
        }

        currentImageName.Value = trialImageData.data.image.name;
        currentImageSize.Value = new Vector2Int(trialImageData.data.image.width, trialImageData.data.image.height);
        hitTarget.Value = trialImageData.data.hitTarget;
    }
}
