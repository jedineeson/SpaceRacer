using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSurvival : TimerBase
{
    [SerializeField]
    private ControllerBase m_Player;

    [SerializeField]
    private float m_StartTimer = 15;

    private float m_SurvivalTimer = 0;

    private void Start()
    {
        m_Timer = m_StartTimer;
    }

    protected override void Update()
    {
        base.Update();
        CheckTimer();
    }

    protected override void UpdateTimer()
    {
        if (m_Player.CanControl)
        {
            m_Timer -= Time.deltaTime;
            m_SurvivalTimer += Time.deltaTime;
        }
    }

    private void CheckTimer()
    {
        if (m_Timer < 0)
        {
            m_Player.EndRun(3);
        }
    }

    protected override void SetEndTimer()
    {
        m_Timer = m_SurvivalTimer;
    }
}
