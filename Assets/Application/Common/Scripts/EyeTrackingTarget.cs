using Obvious.Soap;
//using Shapes;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EyeTrackingTarget : MonoBehaviour
{
    //public Disc disc;

    public Vector3Variable raycastLocalHit;
    public Vector3Variable targetLocalPosition;

    private bool isTrialActive = false;

    private void LateUpdate()
    {
        if (transitioning)
        {
            return;
        }

        transform.localScale = new Vector3(
            1.0f / transform.parent.localScale.x,
            1.0f / transform.parent.localScale.y,
            1.0f / transform.parent.localScale.z
            );

        //transform.localPosition = raycastLocalHit.Value.WithZ(-.01f);
        
        Plane plane = new Plane(transform.parent.forward, transform.parent.position);
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (plane.Raycast(ray, out float enter))
        {
            Vector3 hit = ray.GetPoint(enter);
            transform.position = hit;
        }

        Vector3 clampedLocalPosition = new Vector3(
            Mathf.Clamp(transform.localPosition.x, -0.5f, 0.5f),
            Mathf.Clamp(transform.localPosition.y, -0.5f, 0.5f),
            0
            );

        targetLocalPosition.Value = clampedLocalPosition;
        transform.localPosition = clampedLocalPosition.WithZ(-.01f);

    }

    private bool transitioning = false;

    private IEnumerator PerformTransition(float duration = 1.0f)
    {
        transitioning = true;

        double initial = Time.unscaledTimeAsDouble;
        double third = duration / 3.0;
        //float initialRadius = disc.Radius;
        //float initialThickness = disc.Thickness;

        while (Time.unscaledTimeAsDouble < initial + 0.1)
        {
            float progress = (float)((Time.unscaledTimeAsDouble - initial) / 0.1);
            //disc.Radius = Mathf.Lerp(initialRadius, initialRadius / 10f, progress);
            //disc.Thickness = Mathf.Lerp(initialThickness, 0.1f, progress);
            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        while (Time.unscaledTimeAsDouble < initial + 0.3)
        {
            float progress = (float)((Time.unscaledTimeAsDouble - initial - 0.2) / 0.1);
            //disc.Radius = Mathf.Lerp(initialRadius / 10f, initialRadius, progress);
            //disc.Thickness = Mathf.Lerp(0.1f, initialThickness, progress);
            yield return null;
        }
        yield return new WaitForSeconds(0.7f);
        //disc.Radius = initialRadius;
        //disc.Thickness = initialThickness;

        transitioning = false;
    }

    public void PinchTransition(float duration = 1.0f)
    {
        if (!transitioning)
        {
            StartCoroutine(PerformTransition(duration));
        }
    }
}
