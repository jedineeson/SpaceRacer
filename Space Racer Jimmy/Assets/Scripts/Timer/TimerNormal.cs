using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerNormal : TimerBase
{
    [SerializeField]
    private ControllerBase m_Player;

    protected override void UpdateTimer()
    {
        if (m_Player.CanControl)
        {
            m_Timer += Time.deltaTime;
        }
    }
}
