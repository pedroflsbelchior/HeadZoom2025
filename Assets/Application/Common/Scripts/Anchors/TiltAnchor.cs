using UnityEngine;

[CreateAssetMenu(fileName = "TiltAnchor", menuName = "HeadZoom/TiltAnchor")]
public class TiltAnchor : AnchorObject
{
    public override AnchorTransform CalculateAnchorLocalPosition()
    {
        if (percentage != null)
            percentage.Value = Mathf.Clamp(headZoomCurrentDistance.Value switch
            {
                _ when headZoomCurrentDistance.Value > 0 => 
                    headZoomCurrentDistance.Value / headZoomForwardDistance.Value,
                _ when headZoomCurrentDistance.Value < 0 => 
                    headZoomCurrentDistance.Value / headZoomNegativeDistance.Value,
                _ => 0
            }, -1, 1);


        float distance = headZoomCurrentDistance.Value switch
        {
            _ when headZoomCurrentDistance.Value > 0 => Mathf.Clamp((defaultDistance - headZoomForwardDistance.Value - minimumHeadDistance) * headZoomCurrentDistance.Value / headZoomForwardDistance.Value + headZoomCurrentDistance.Value, 0, defaultDistance - minimumHeadDistance),
            _ when headZoomCurrentDistance.Value < 0 => headZoomCurrentDistance.Value / headZoomNegativeDistance.Value,
            _ => 0
        };



        return new AnchorTransform
        {
            position =
                headPosition.Value +
                headForward.Value.normalized * (defaultDistance - distance),
            forward = headForward.Value,
            up = headUp.Value,
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
