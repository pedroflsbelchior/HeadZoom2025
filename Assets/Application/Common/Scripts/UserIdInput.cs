using Obvious.Soap;
using System;
using UnityEngine;
using UnityEngine.Events;

public class UserIdInput : MonoBehaviour
{
    public IntVariable userId;

    [Header("Events")]
    public UnityEvent<string> onIdChanged;
    public UnityEvent onValidId;
    public UnityEvent onInvalidId;

    private void OnEnable()
    {
        if (userId == null)
        {
            Debug.LogError("UserIdInput: userId is not set");
            return;
        }
        userId.OnValueChanged += OnUserIdChanged;
        OnUserIdChanged(userId.Value);
    }

    private void OnDisable()
    {
        if (userId == null)
        {
            return;
        }
        userId.OnValueChanged -= OnUserIdChanged;
    }

    private void OnUserIdChanged(int obj)
    {
        if (obj == 0)
        {
            onIdChanged.Invoke("");
            onInvalidId.Invoke();
        }
        else
        {
            onIdChanged.Invoke(obj.ToString());
            onValidId.Invoke();
        }
    }

    public void AddDigit(int digit)
    {
        userId.Value = userId.Value * 10 + digit;
    }

    public void RemoveDigit()
    {
        userId.Value = userId.Value / 10;
    }
}
