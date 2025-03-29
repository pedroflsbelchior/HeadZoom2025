using Obvious.Soap;
using Oculus.Interaction.Input;
using UnityEngine;

public class PinchDetector : MonoBehaviour
{
    public Hand hand;
    public BoolVariable isPinching;

    private void Update()
    {
        isPinching.Value = hand.GetIndexFingerIsPinching();
    }
}
