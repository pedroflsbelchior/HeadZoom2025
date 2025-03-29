using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public enum HapticController
{
    none,
    left,
    right,
    both
}

public class HapticFeedback : MonoBehaviour
{
    [SerializeField] private AnimationCurve intensityCurve = AnimationCurve.Linear(0, 1, 1, 1);
    [SerializeField] private AnimationCurve amplitudeCurve = AnimationCurve.Linear(0, 1, 1, 1);
    //[Range(0, 1)]
    //public float intensity = 1;
    //[Range(0, 1)]
    //public float amplitude = 1;
    [Range(0, 10)]
    public float duration = 0.1f;
    public HapticController controllers;

    public bool playOnEnable;

    private Coroutine hapticCoroutine;

    private IEnumerator Haptic()
    {
        float time = 0;

        if (controllers == HapticController.none)
            yield break;

        float totalIntensityCurveTime = intensityCurve.keys[intensityCurve.length - 1].time;
        float totalAmplitudeCurveTime = amplitudeCurve.keys[amplitudeCurve.length - 1].time;

        while (time < duration)
        {
            time += Time.deltaTime;
            
            float intensity = intensityCurve.Evaluate(time % totalIntensityCurveTime);
            float amplitude = amplitudeCurve.Evaluate(time % totalAmplitudeCurveTime);

            if (controllers == HapticController.left || controllers == HapticController.both)
                OVRInput.SetControllerVibration(intensity, amplitude, OVRInput.Controller.LTouch);
            if (controllers == HapticController.right || controllers == HapticController.both)
                OVRInput.SetControllerVibration(intensity, amplitude, OVRInput.Controller.RTouch);

            yield return null;
        }

        ZeroHaptics();
    }

    public void StartHaptic()
    {
        if (hapticCoroutine != null)
            StopCoroutine(hapticCoroutine);

        hapticCoroutine = StartCoroutine(Haptic());
    }

    public void StopHaptic()
    {
        if (hapticCoroutine != null)
            StopCoroutine(hapticCoroutine);

        ZeroHaptics();
    }

    private void ZeroHaptics()
    {
        if (controllers == HapticController.left || controllers == HapticController.both)
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
        if (controllers == HapticController.right || controllers == HapticController.both)
            OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }

    private void OnEnable()
    {
        if (playOnEnable)
            StartHaptic();
    }

    private void OnDisable()
    {
        StopHaptic();
    }

}
