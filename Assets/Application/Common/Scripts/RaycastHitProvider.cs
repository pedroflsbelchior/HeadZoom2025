using UnityEngine;
using Obvious.Soap;

public class RaycastHitProvider : MonoBehaviour
{
    [Header("Raycast Inputs")]
    public Vector3Variable origin;
    public Vector3Variable direction;
    [Header("Raycast Outputs")]
    public Vector3Variable worldHit;
    public Vector3Variable localHit;

    private Ray ray = new Ray();
    
    public void OnEnable()
    {
        origin.OnValueChanged += UpdateRayOrigin;
        direction.OnValueChanged += UpdateRayDirection;
    }

    public void OnDisable()
    {
        origin.OnValueChanged -= UpdateRayOrigin;
        direction.OnValueChanged -= UpdateRayDirection;
    }

    private void UpdateRayOrigin(Vector3 o) => ray.origin = o;
    private void UpdateRayDirection(Vector3 d) => ray.direction = d;

    void Update()
    {
        Plane p = new Plane(transform.forward, transform.position);
        if (p.Raycast(ray, out float distance))
        {
            var position = ray.GetPoint(distance);
            worldHit.Value = position;
            localHit.Value = transform.InverseTransformPoint(position);
        }
        else
        {
            //Debug.Log("NOT HITTING");
        }
    }
}
