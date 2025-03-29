using UnityEngine;

[CreateAssetMenu(fileName = "ParallelAnchor", menuName = "HeadZoom/ParallelAnchor")]
public class ParallelAnchor : AnchorObject
{
    public override AnchorTransform CalculateAnchorLocalPosition()
    {
        //Debug.Log(headForward.Value);

        float distance = headZoomCurrentDistance.Value switch
        {
            _ when headZoomCurrentDistance.Value > 0 => Mathf.Clamp((defaultDistance - headZoomForwardDistance.Value - minimumHeadDistance) * headZoomCurrentDistance.Value / headZoomForwardDistance.Value + headZoomCurrentDistance.Value, 0, defaultDistance - minimumHeadDistance),
            _ when headZoomCurrentDistance.Value < 0 => headZoomCurrentDistance.Value / headZoomNegativeDistance.Value,
            _ => 0
        };

        return new AnchorTransform
        {
            position = defaultHeadPosition.Value +
                defaultHeadForward.Value.normalized * (defaultDistance - distance),
            forward = defaultHeadForward.Value,
            up = Vector3.up,
            hit = raycastLocalHit.Value

        };
    }

    public override Vector3 headPanCoordinates(Vector3 plane)
    {
        return Vector3.zero;
    }

    public override float headZoomDistance()
    {
        return 0;
    }

    public override bool isStaticAnchor() => false;
}
