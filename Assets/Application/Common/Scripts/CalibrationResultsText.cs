using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;

public class CalibrationResultsText : MonoBehaviour
{
    public FloatVariable forward;
    public FloatVariable backward;

    [Header("Events")]
    public UnityEvent<string> onTextChange;
    private void OnEnable()
    {
        SetText();
    }

    private void OnDisable()
    {
        
    }

    public void SetText()
    {
        string s = $"<color=\"yellow\">{forward.Value * 100:00}</color> cm forward\r\n<color=\"orange\">{backward.Value * 100:00}</color> cm backward\r\n\r\n<i>Start trial?</i>";
        onTextChange.Invoke(s);
    }
}
