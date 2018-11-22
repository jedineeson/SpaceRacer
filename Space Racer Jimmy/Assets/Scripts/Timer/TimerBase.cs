using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBase : MonoBehaviour
{
    protected float m_Timer = 0f;
    public float Timer
    {
        get { return m_Timer; }
        set { m_Timer += value; }
    }

    protected virtual void Update ()
    {
        UpdateTimer();
    }

    protected virtual void UpdateTimer()
    {

    }

    protected virtual void SetEndTimer()
    {

    }

    public virtual void EndTimer()
    {
        SetEndTimer();
    }
}
