using Obvious.Soap;
using UnityEngine;

public class AlignWithHeadOnTrialStart : MonoBehaviour
{
    public Vector3Variable headDefaultPosition;
    public Vector3Variable headDefaultForward;

    private void OnEnable()
    {
        headDefaultPosition.OnValueChanged += OnHeadDefaultPositionChanged;
        headDefaultForward.OnValueChanged += OnHeadDefaultForwardChanged;
    }

    private void OnDisable()
    {
        headDefaultPosition.OnValueChanged -= OnHeadDefaultPositionChanged;
        headDefaultForward.OnValueChanged -= OnHeadDefaultForwardChanged;
    }

    private void OnHeadDefaultPositionChanged(Vector3 vector)
    {
        SetPositionAndForward();
    }

    private void OnHeadDefaultForwardChanged(Vector3 vector)
    {
        SetPositionAndForward();
    }

    private void SetPositionAndForward()
    {
        transform.position = headDefaultPosition.Value + headDefaultForward.Value.normalized * 2;
        transform.forward = headDefaultForward.Value.normalized;
    }
}
