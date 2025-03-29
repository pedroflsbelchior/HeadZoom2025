using Obvious.Soap;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    public AnchorObject anchorObject = null;

    public string AnchorType => anchorObject.AnchorType;

    public AnchorTransform CalculateAnchorLocalPosition()
        => anchorObject.CalculateAnchorLocalPosition();
    public Vector3 headPanCoordinates(Vector3 plane)
        => anchorObject.headPanCoordinates(plane);
    public float headZoomDistance()
        => anchorObject.headZoomDistance();
    public bool isStaticAnchor() 
        => anchorObject.isStaticAnchor();
}
