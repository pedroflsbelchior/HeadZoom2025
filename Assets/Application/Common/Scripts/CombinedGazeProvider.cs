using Obvious.Soap;
using UnityEngine;

public class CombinedGazeProvider : MonoBehaviour
{
    [Header("Left Eye Inputs")]
    public RayVariable LeftEyeRay;
    [Header("Right Eye Inputs")]
    public RayVariable RightEyeRay;
    [Header("Combined Gaze Output")]
    public RayVariable CombinedGazeRay;

    public bool overrideGaze = false;

    private void LateUpdate()
    {
        
        if (overrideGaze)
        {
            CombinedGazeRay.Value = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
            return;
        }

        Vector3 gazePosition = (LeftEyeRay.Value.origin + RightEyeRay.Value.origin) * 0.5f;
        Vector3 LeftGazePoint = LeftEyeRay.Value.origin + LeftEyeRay.Value.direction.normalized;
        Vector3 RightGazePoint = RightEyeRay.Value.origin + RightEyeRay.Value.direction.normalized;
        Vector3 gazePoint = (LeftGazePoint + RightGazePoint) * 0.5f;

        CombinedGazeRay.Value = new Ray(gazePosition, gazePoint - gazePosition);
    }

}
