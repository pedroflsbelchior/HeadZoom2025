using Obvious.Soap;
using UnityEngine;

public class ControllersInputModule : MonoBehaviour
{
    public BoolVariable leftTrigger;
    public BoolVariable rightTrigger;
    public BoolVariable leftGrip;
    public BoolVariable rightGrip;

    public void SetVariable(BoolVariable variable, OVRInput.Button button)
    {
        variable.Value = OVRInput.GetDown(button) || OVRInput.Get(button);
    }

    public void Update()
    {
        SetVariable(rightTrigger, OVRInput.Button.PrimaryIndexTrigger);
        SetVariable(leftTrigger, OVRInput.Button.SecondaryIndexTrigger);
        //SetVariable(rightGrip, OVRInput.Button.PrimaryHandTrigger);
        //SetVariable(leftGrip, OVRInput.Button.SecondaryHandTrigger);
    }
}
