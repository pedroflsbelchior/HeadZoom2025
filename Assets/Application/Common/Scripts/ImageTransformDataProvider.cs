using UnityEngine;
using UnityEngine.Events;

public class ImageTransformDataProvider : MonoBehaviour
{
    public UnityEvent<Vector3> onPositionChange;
    public UnityEvent<Vector3> onForwardChange;
    public UnityEvent<Vector3> onUpChange;
    public UnityEvent<Vector3> onScaleChange;

    private void LateUpdate()
    {
        if (transform.hasChanged)
        {
            transform.hasChanged = false;
            NotifyChange();
        }
    }

    private void NotifyChange()
    {
        onPositionChange.Invoke(transform.position);
        onForwardChange.Invoke(transform.forward);
        onUpChange.Invoke(transform.up);
        onScaleChange.Invoke(transform.localScale);
    }
}
