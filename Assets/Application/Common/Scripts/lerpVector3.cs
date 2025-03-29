using UnityEngine;

public class lerpVector3 : lerpCore<Vector3>
{
    public override Vector3 Lerp(Vector3 a, Vector3 b, float t)
    {
        return Vector3.Lerp(a, b, t);
    }
}