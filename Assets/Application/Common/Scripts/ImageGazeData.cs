using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;

public class ImageGazeData : MonoBehaviour
{
    public Vector3Variable imagePosition;
    public Vector3Variable imageForward;
    public Vector3Variable imageUp;
    public Vector3Variable imageScale;
    public Vector2IntVariable imageSize;

    public UnityEvent<Vector3> onHit;

    public void Gaze(Ray gazeRay)
    {
        Plane plane = new Plane(imageForward.Value, imagePosition.Value);
        Quaternion rotation = Quaternion.LookRotation(imageForward.Value, imageUp.Value);
        transform.position = imagePosition.Value;
        transform.rotation = rotation;
        transform.localScale = new Vector3(imageScale.Value.x, imageScale.Value.y, 1);

        if (plane.Raycast(gazeRay, out float distance))
        {
            Vector3 hitPoint = gazeRay.GetPoint(distance);
            Vector3 localHitPoint = transform.InverseTransformPoint(hitPoint);
            onHit.Invoke(localHitPoint);
        }
    }
}
