using JetBrains.Annotations;
using Obvious.Soap;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CurrentImageUserGrade : MonoBehaviour
{

    public IntVariable UserGrade;
    public ToggleGroup _ToggleGroup;
    public ToggleGroup ToggleGroup
    {
        get
        {
            if (_ToggleGroup == null)
                _ToggleGroup = GetComponent<ToggleGroup>();
            return _ToggleGroup;
        }
    }
    public UnityEvent onSetGrade;
    public UnityEvent onUnsetGrade;

    private void OnEnable()
    {
        UserGrade.Value = 0;
        ToggleGroup.SetAllTogglesOff();
        foreach (var toggle in ToggleGroup.ActiveToggles())
           toggle.onValueChanged.AddListener(OnGrade);
    }

    private void OnDisable()
    {
        foreach (var toggle in ToggleGroup.ActiveToggles())
            toggle.onValueChanged.RemoveListener(OnGrade);
    }

    public void OnGrade(bool hasGraded)
    {
        if (ToggleGroup == null)
        {
            Debug.LogError("ToggleGroup not found");
            return;
        }

        if (!hasGraded && !ToggleGroup.AnyTogglesOn())
        {
            UserGrade.Value = 0;
            onUnsetGrade.Invoke();
        }
        else
        {
            onSetGrade.Invoke();
        }
    }
}
