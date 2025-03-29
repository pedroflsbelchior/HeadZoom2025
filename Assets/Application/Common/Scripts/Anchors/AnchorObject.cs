using Obvious.Soap;
using UnityEngine;

//[CreateAssetMenu(fileName = "AnchorObject", menuName = "Scriptable Objects/AnchorObject")]
public class AnchorObject : ScriptableObject, IAnchor
{
    [Header("Configuration")]
    public string AnchorType = "Static";
    [Space(10)]
    public float defaultDistance = 1.0f;
    public float minimumHeadDistance = 0.2f;
    public Vector3 raycastTargetSize = Vector3.one;

    public FilterData filterData;

    [Header("Scriptable Variables")]
    public FloatVariable headZoomCurrentDistance;
    public FloatVariable headZoomForwardDistance;
    public FloatVariable headZoomNegativeDistance;
    public Vector3Variable defaultHeadPosition;
    public Vector3Variable defaultHeadForward;
    public Vector3Variable headPosition;
    public Vector3Variable headForward;
    public Vector3Variable headUp;
    public Vector3Variable raycastWorldHit;
    public Vector3Variable raycastLocalHit;

    public FloatVariable percentage;

    public virtual AnchorTransform CalculateAnchorLocalPosition()
    {
        percentage.Value = 0;
        return new AnchorTransform
        {
            position = defaultHeadPosition.Value + defaultHeadForward.Value,
            forward = defaultHeadForward.Value,
            up = Vector3.up,
            hit = Vector2.zero
        };
    }

    public virtual Vector3 headPanCoordinates(Vector3 plane)
    {
        return Vector3.zero;
    }

    public virtual float headZoomDistance()
    {
        return 0;
    }

    public virtual bool isStaticAnchor() => true;
}
