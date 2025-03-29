using Obvious.Soap;
using UnityEngine;

public class KalmanFilterFloatProvider : MonoBehaviour
{
    public FloatVariable input;
    [Space(10)]
    public FloatVariable output;
    [Header("Parameters")]
    public FloatVariable Q;
    public FloatVariable R;
    [Space(10)]
    public TrialImageDataVariable trialImageData;

    private KalmanFilterFloat filter;

    private void OnEnable()
    {
        InitializeFilter();

        trialImageData.OnValueChanged += OnTrialImageDataChanged;
        input.OnValueChanged += OnInputValueChanged;
        Q.OnValueChanged += OnQValueChanged;
        R.OnValueChanged += OnRValueChanged;
    }

    private void OnDisable()
    {
        trialImageData.OnValueChanged -= OnTrialImageDataChanged;
        input.OnValueChanged -= OnInputValueChanged;
        Q.OnValueChanged -= OnQValueChanged;
        R.OnValueChanged -= OnRValueChanged;
    }

    private void OnQValueChanged(float obj) => InitializeFilter();
    private void OnRValueChanged(float obj) => InitializeFilter();

    private void InitializeFilter()
    {
        if (Q.Value == 0 || R.Value == 0)
            filter = null;
        else
            filter = new KalmanFilterFloat(Q.Value, R.Value);
    }

    private void OnTrialImageDataChanged(TrialImageData data)
    {
        if (filter == null)
            return;
        filter.Reset();
    }

    private void OnInputValueChanged(float value)
    {
        if (filter == null)
            return;
        output.Value = filter.Update(value, Q.Value, R.Value);
    }
}
