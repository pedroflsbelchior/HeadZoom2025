using System;
using UnityEngine;
using UnityEngine.Events;

public static class RendererExtensions
{
    public static void SetMaterialTexture(this Renderer renderer, Texture2D texture)
    {
        if (renderer == null || renderer.material == null)
            return;
        renderer.material.mainTexture = texture;
    }

    public static void SetMaterialTexture(this MeshRenderer renderer, Texture2D texture)
    {
        if (renderer == null || renderer.material == null)
            return;
        renderer.material.mainTexture = texture;
    }
}

public class TrialImageDataListener : MonoBehaviour
{
    [Header("Trial Image Data")]
    public TrialImageDataVariable trialImageData;
    [Header("Events")]
    public UnityEvent<AnchorObject> onNewImageAnchor;
    [Space(10)]
    public UnityEvent<ImageDifficulty> onNewImageDifficulty;
    [Space(10)] 
    public UnityEvent<Texture2D> onNewImageTexture;
    [Space(10)]
    public UnityEvent<string> onNewImageName;
    [Space(10)]
    public UnityEvent<Vector2> onNewImageHitTarget;

    private void OnEnable()
        => trialImageData.OnValueChanged += OnTrialImageDataChanged;
    private void OnDisable()
        => trialImageData.OnValueChanged -= OnTrialImageDataChanged;

    private void OnTrialImageDataChanged(TrialImageData data)
    {
        if (data == null || data.data == null)
        {
            onNewImageAnchor.Invoke(null);
            onNewImageDifficulty.Invoke(ImageDifficulty.Easy);
            onNewImageTexture.Invoke(null);
            onNewImageName.Invoke(string.Empty);
            onNewImageHitTarget.Invoke(new Vector2(float.NaN,float.NaN));
            return;
        }
        //EventHook hook = new EventHook("TrialImageDataListener");
        //EventBus.Trigger(hook);
        onNewImageAnchor.Invoke(data.anchor);
        onNewImageDifficulty.Invoke(data.difficulty);
        onNewImageTexture.Invoke(data.data.image);
        onNewImageName.Invoke(data.data.image.name);
        onNewImageHitTarget.Invoke(data.data.hitTarget);
    }
}
