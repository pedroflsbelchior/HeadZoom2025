using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DelayBeforeInteractable : MonoBehaviour
{
    public float delayInSeconds = 1.0f;

    IEnumerator DisableButtonsForSeconds(float seconds)
    {
        var buttons = GetComponentsInChildren<Button>(true);

        foreach (var button in buttons)
            button.interactable = false;

        yield return new WaitForSeconds(seconds);

        foreach (var button in buttons)
            button.interactable = true;
    }

    private Coroutine coroutine = null;

    private void OnEnable()
    {
        if (coroutine != null)
            StopCoroutine(coroutine);

        if (Time.frameCount > 10)
            coroutine = StartCoroutine(DisableButtonsForSeconds(delayInSeconds));
    }
}
