using Obvious.Soap;
using System;
using UnityEngine;
using UnityEngine.Events;

public class CurrentImageUserGradeEvent : MonoBehaviour
{
    public IntVariable UserGrade;

    public UnityEvent onGradeSet;
    public UnityEvent onGradeUnset;

    public void OnEnable()
    {
        UserGrade.OnValueChanged += OnUserGradeChanged;
    }

    public void OnDisable()
    {
        UserGrade.OnValueChanged -= OnUserGradeChanged;
    }

    private void OnUserGradeChanged(int grade)
    {
        if (grade > 0)
        {
            onGradeSet.Invoke();
        }
        else
        {
            onGradeUnset.Invoke();
        }
    }
}
