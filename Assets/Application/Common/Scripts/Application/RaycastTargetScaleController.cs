using UnityEngine;
using UnityEngine.Events;

public class RaycastTargetScaleController : MonoBehaviour
{
    [SerializeField]
    private Transform _targetTransform;
    [Header("Events")]
    public UnityEvent<Vector3> onSizeChange;


    public Transform targetTransform { 
        get { 
            return (_targetTransform == null) ? transform : _targetTransform; } }

    public void SetScale(AnchorObject anchor)
    {
        if (anchor == null)
            return;
        targetTransform.localScale = anchor.raycastTargetSize;
        onSizeChange.Invoke(anchor.raycastTargetSize);
    }
}
