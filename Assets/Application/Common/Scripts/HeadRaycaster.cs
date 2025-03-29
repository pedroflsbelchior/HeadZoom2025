using UnityEngine;
using Obvious.Soap;

public class HeadRaycaster : MonoBehaviour
{
    public Vector3Variable headPosition;
    public Vector3Variable headDirection;
    public Vector3Variable headUp;

    void Update()
    {
        headPosition.Value = transform.position;
        headDirection.Value = transform.forward;
        headUp.Value = transform.up;
    }
}
