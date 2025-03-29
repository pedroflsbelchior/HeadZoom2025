using Obvious.Soap;
using UnityEngine;

public class OVRHeadsetInputModule : MonoBehaviour
{
    public BoolVariable userPresent;

    private void OnEnable()
    {
        userPresent.Value = OVRPlugin.userPresent;

        OVRManager.HMDMounted += OnHMDMounted;
        OVRManager.HMDUnmounted += OnHMDUnmounted;
    }

    private void OnHMDUnmounted()
    {
        userPresent.Value = false;
    }

    private void OnHMDMounted()
    {
        userPresent.Value = true;
    }
}
