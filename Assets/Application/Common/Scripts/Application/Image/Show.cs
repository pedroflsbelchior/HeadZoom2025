using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Show : MonoBehaviour
{
    public float ShowDuration = 1.0f;
    public float HideDuration = 1.0f;

    public UnityEvent<float> onAlphaChange;

    private float alpha = 0;
    Coroutine coroutine = null;

    public void StartShow()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(show());
    }

    public void StartHide()
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        coroutine = StartCoroutine(hide());
    }

    private IEnumerator show()
    {
        while (alpha < 1)
        {
            alpha = Mathf.Clamp01(alpha + Time.deltaTime / ShowDuration);
            onAlphaChange.Invoke(alpha);
            yield return null;
        }
    }

    private IEnumerator hide()
    {
        while (alpha > 0)
        {
            alpha = Mathf.Clamp01(alpha - Time.deltaTime / HideDuration);
            onAlphaChange.Invoke(alpha);
            yield return null;
        }
    }
}
