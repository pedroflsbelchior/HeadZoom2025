using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Enum representing the origin of the pixel grid.
/// </summary>
public enum PixelOrigin
{
    None,
    BottomLeft,
    TopLeft,
    TopRight,
    BottomRight
}

/// <summary>
/// Class to handle raycast hits in pixel coordinates.
/// </summary>
public class RaycastHitInPixels : MonoBehaviour
{
    [Header("Image Size In Pixels")]
    /// <summary>
    /// The size of the image in pixels.
    /// </summary>
    public Vector2Int imageSizeInPixels { get; set; } = Vector2Int.zero;

    [Tooltip("The origin of the pixel grid")]
    [SerializeField] private PixelOrigin imageOrigin = PixelOrigin.BottomLeft;

    [Header("Events")]
    /// <summary>
    /// Event triggered when a pixel is hit.
    /// </summary>
    [SerializeField] private UnityEvent<Vector2Int> onPixelHit = new();

    /// <summary>
    /// Converts a hit point to pixel coordinates based on the image size and origin.
    /// </summary>
    /// <param name="hitPoint">The hit point in normalized coordinates (-0.5 to 0.5).</param>
    /// <param name="sizeInPixels">The size of the image in pixels.</param>
    /// <param name="pixelOrigin">The origin of the pixel grid.</param>
    /// <returns>The hit point in pixel coordinates.</returns>
    private Vector2Int OnHit(Vector2 hitPoint, Vector2Int sizeInPixels, PixelOrigin pixelOrigin)
    {
        Vector2 refactoredHit = pixelOrigin switch
        {
            PixelOrigin.TopLeft => new Vector2(hitPoint.x + 0.5f, 0.5f - hitPoint.y),
            PixelOrigin.TopRight => new Vector2(0.5f - hitPoint.x, 0.5f - hitPoint.y),
            PixelOrigin.BottomRight => new Vector2(0.5f - hitPoint.x, hitPoint.y - 0.5f),
            _ => new Vector2(hitPoint.x + 0.5f, hitPoint.y - 0.5f)
        };

        refactoredHit.Scale(sizeInPixels);
        return Vector2Int.FloorToInt(refactoredHit);
    }

    /// <summary>
    /// Invokes the onPixelHit event with the hit point in pixel coordinates.
    /// </summary>
    /// <param name="hitPoint">The hit point in normalized coordinates (-0.5 to 0.5).</param>
    public void OnHit(Vector2 hitPoint)
    {
        onPixelHit.Invoke(OnHit(hitPoint, imageSizeInPixels, imageOrigin));
    }

    /// <summary>
    /// Invokes the onPixelHit event with the hit point in pixel coordinates.
    /// </summary>
    /// <param name="hitPoint">The hit point in normalized coordinates (-0.5 to 0.5).</param>
    public void OnHit(Vector3 hitPoint)
    {
        onPixelHit.Invoke(OnHit(hitPoint, imageSizeInPixels, imageOrigin));
    }
}
