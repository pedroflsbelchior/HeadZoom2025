using Obvious.Soap;
using System;
using UnityEngine;

public class HitHoverProvider : MonoBehaviour
{
    public BoolVariable isHovering;
    [Space(10)]
    public Vector2Variable imageHitCoordinates;
    public Vector3Variable targetLocalPosition;
    public FloatVariable hitRadius;

    private void OnEnable()
    {
        imageHitCoordinates.OnValueChanged += OnImageHitCoordinatesChanged;
        targetLocalPosition.OnValueChanged += OnTargetLocalPosition;
    }

    private void OnDisable()
    {
        imageHitCoordinates.OnValueChanged -= OnImageHitCoordinatesChanged;
        targetLocalPosition.OnValueChanged -= OnTargetLocalPosition;
    }

    private void OnImageHitCoordinatesChanged(Vector2 vector)
    {
        CheckHit();
    }

    private void OnTargetLocalPosition(Vector3 vector)
    {
        CheckHit();
    }

    private void CheckHit()
    {
        var distance = Vector3.Distance(
            transform.TransformPoint(imageHitCoordinates.Value),
            transform.TransformPoint(targetLocalPosition.Value));

        
        //Debug.Log($"Image Hit Coordinates: {imageHitCoordinates.Value}");
        //Debug.Log($"Target Local Position: {targetLocalPosition.Value}");
        //Debug.Log(distance);
        isHovering.Value =  distance <= hitRadius.Value;
    }
}
