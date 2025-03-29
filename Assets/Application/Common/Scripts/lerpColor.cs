using UnityEngine;

public class lerpColor : lerpCore<Color>
{
    public override Color Lerp(Color a, Color b, float t)
    {
        return Color.Lerp(a, b, t);
    }
}