using Obvious.Soap;
using System;
using UnityEngine;
using UnityEngine.Events;

public class DoublePinch : MonoBehaviour
{
    [Header("Variables")]
    public BoolVariable rightPinch;
    public BoolVariable leftPinch;
    [Header("Events")]
    public UnityEvent<bool> onRightDoublePinch;
    public UnityEvent<bool> onLeftDoublePinch;

    private void OnEnable()
    {
        rightPinch.OnValueChanged += RightPinch;
        leftPinch.OnValueChanged += LeftPinch;
    }

    private void OnDisable()
    {
        rightPinch.OnValueChanged -= RightPinch;
        leftPinch.OnValueChanged -= LeftPinch;
    }

    private double lastRightPinchTime = 0;
    private double lastLeftPinchTime = 0;

    private bool isDoublePinchingRight = false;
    private bool isDoublePinchingLeft = false;

    private void RightPinch(bool obj)
    {
        if (!obj)
        {
            if (isDoublePinchingRight)
            {
                isDoublePinchingRight = false;
                onRightDoublePinch.Invoke(false);
            }
            else
            {
                if (Time.unscaledTimeAsDouble - lastRightPinchTime < 0.5)
                {
                    isDoublePinchingRight = true;
                    onRightDoublePinch.Invoke(true);
                    lastRightPinchTime = 0;
                }
            }
            return;
        }
        
        lastRightPinchTime = Time.unscaledTimeAsDouble;
    }

    private void LeftPinch(bool obj)
    {
        if (!obj)
        {
            if (isDoublePinchingLeft)
            {
                isDoublePinchingLeft = false;
                onLeftDoublePinch.Invoke(false);
            }
            else
            {
                if (Time.unscaledTimeAsDouble - lastLeftPinchTime < 0.5)
                {
                    isDoublePinchingLeft = true;
                    onLeftDoublePinch.Invoke(true);
                    lastLeftPinchTime = 0;
                }
            }
            return;
        }

        lastLeftPinchTime = Time.unscaledTimeAsDouble;
    }
}
