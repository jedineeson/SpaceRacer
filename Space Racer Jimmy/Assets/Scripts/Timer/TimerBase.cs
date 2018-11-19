﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerBase : MonoBehaviour
{
    protected float m_Timer = 0f;
    public float Timer
    {
        get { return m_Timer; }
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

    public virtual void TimeBonus(float aBonus)
    {
        m_Timer += aBonus;
    }

    public virtual void EndTimer()
    {
        SetEndTimer();
    }
}