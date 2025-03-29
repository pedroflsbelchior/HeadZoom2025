using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;

public class AnchorConsumer : MonoBehaviour
{
    public AnchorObject anchor = null;
    public StringVariable anchorType;

    public void SetAnchor(AnchorObject anchor)
    {
        this.anchor = anchor;
        anchorType.Value = this.anchor == null ? "None" : anchor.AnchorType;
    }

    void LateUpdate()
    {
        if (anchor == null)
        {
            return;
        }


        AnchorTransform anchorTransform = anchor.CalculateAnchorLocalPosition();

        //Debug.Log("AnchorTransformHit: " + anchorTransform.hit);

        Vector3 hit = new Vector3(
            Mathf.Clamp(-anchorTransform.hit.x,-0.5f,.5f), 
            Mathf.Clamp(-anchorTransform.hit.y,-0.5f,.5f), 0);
        transform.position = anchorTransform.position;
        transform.rotation = Quaternion.LookRotation(anchorTransform.forward, anchorTransform.up);
        transform.position = transform.TransformPoint(hit);
    }
}
