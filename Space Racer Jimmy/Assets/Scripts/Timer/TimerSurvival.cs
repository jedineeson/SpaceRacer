using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerSurvival : TimerBase
{
    [SerializeField]
    private float m_StartTimer = 15;

    private float m_SurvivalTimer = 0;

    protected override void Start()
    {
        m_Timer = m_StartTimer;
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        CheckTimer();
    }

    protected override void UpdateTimer()
    {
        if (GameManager.Instance.ShipController)
        {
            if (GameManager.Instance.ShipController.CanControl)
            {
                m_Timer -= Time.deltaTime;
                if (m_Timer > 0)
                {
                    m_SurvivalTimer += Time.deltaTime;
                }
            }
        }
    }
    
    private void CheckTimer()
    {
        if (m_Timer < 0)
        {
            GameManager.Instance.ShipController.EndRun(3);
        }
    }

    protected override void SetEndTimer()
    {
        m_Timer = m_SurvivalTimer;
    }
}
