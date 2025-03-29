using UnityEngine;

public class AnchorTransform
{
    public Vector3 position;
    public Vector3 forward;
    public Vector3 up;
    public Vector2 hit;
}

public interface IAnchor
{
    public float headZoomDistance();
    public Vector3 headPanCoordinates(Vector3 plane);
    public bool isStaticAnchor();
    public AnchorTransform CalculateAnchorLocalPosition();
}
