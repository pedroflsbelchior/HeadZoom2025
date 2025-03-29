using Obvious.Soap;
using UnityEngine;

public class TimestampProvider : MonoBehaviour
{
    public IntVariable frames;
    public DoubleVariable time;

    void Update()
    {
        frames.Value = Time.frameCount;
        time.Value = Time.unscaledTimeAsDouble;
    }
}
