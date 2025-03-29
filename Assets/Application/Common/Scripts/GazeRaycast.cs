using UnityEngine;
using UnityEngine.Events;

public class GazeRaycast : MonoBehaviour
{
    public Transform _target;
    public Transform target
    {
        get
        {
            if (_target == null)
                _target = transform;
            return _target;
        }
    }

    public UnityEvent<Vector3> onImageLocalGazeHit;

    public void Raycast(Ray gazeRay)
    {
        Plane plane = new Plane(target.forward, target.position);
        if (plane.Raycast(gazeRay, out float distance))
        {
            Vector3 hit = gazeRay.GetPoint(distance);
            onImageLocalGazeHit.Invoke(target.InverseTransformPoint(hit));
        }
    }
}
