using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerNormal : TimerBase
{
    protected override void UpdateTimer()
    {
        if (GameManager.Instance.ShipController != null)
        {
            if (GameManager.Instance.ShipController.CanControl)
            {
                m_Timer += Time.deltaTime;
            }
        }
    }
}
