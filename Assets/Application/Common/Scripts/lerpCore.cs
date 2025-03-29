using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class lerpCore<T> : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private AnimationCurve curve = new AnimationCurve();
    [SerializeField] private bool animateOnEnable = true;
    [Header("Values")]
    [SerializeField] private T from;
    [SerializeField] private T to;
    [Header("Events")]
    [SerializeField] private UnityEvent<T> onValueChange;

    public virtual T Lerp(T a, T b, float t)
    {
        return default(T);
    }

    private Coroutine coroutine = null;

    private IEnumerator Animate()
    {
        float time = 0;
        if (curve.length == 0)
            yield break;
        if (curve.keys[curve.length - 1].time == 0)
            yield break;

        while (time < curve.keys[curve.length - 1].time)
        {
            onValueChange.Invoke(Lerp(from, to, curve.Evaluate(time)));
            time += Time.deltaTime;
            yield return null;
        }
    }

    public void StartAnimation()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = StartCoroutine(Animate());
    }

    public void StopAnimation()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);
        coroutine = null;
    }

    public void OnEnable()
    {
        if (animateOnEnable)
            StartCoroutine(Animate());
    }

    public void OnDisable()
    {
        StopAnimation();
    }
}
