using UnityEngine;

public enum ImageDifficulty
{
    Easy,
    Medium,
    Hard
}

[CreateAssetMenu(fileName = "TrialImage", menuName = "HeadZoom/TrialImageData")]
public class TrialImage : ScriptableObject
{
    public ImageDifficulty difficulty { get => _difficulty; }
    public Texture2D image { get => _image; }
    public Vector2 hitTarget { get => _hitTarget; }

    [SerializeField]
    protected ImageDifficulty _difficulty;
    [SerializeField]
    protected Texture2D _image = null;

    [Space(10)]
    [SerializeField]
    private Vector2 _hitTarget;
}
