using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalShipController : ControllerBase
{
    [SerializeField]
    private float m_StartTimer = 15;

    private float m_SurvivalTimer = 0;
    

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.ShipController = this;
        m_Timer = m_StartTimer;
    }

    protected override void Update()
    {
        base.Update();
        CheckTimer();
    }

    protected override void EndRun(int aLevel)
    {
        ScoreManager.Instance.UpdateScoreList(aLevel, m_SurvivalTimer);
        GameManager.Instance.TunnelGenerator.ReturnStuff();
        base.EndRun(aLevel);
    }

    protected override void UpdateTimer()
    {
        if (m_CanControl)
        {
            m_Timer -= Time.deltaTime;
            m_SurvivalTimer += Time.deltaTime;
        }
    }

    private void OnCollisionExit(Collision aOther)
    {
        if (m_Hp == 0)
        {
            EndRun(3);
        }
    }

    private void OnTriggerExit(Collider aOther)
    {
        if (m_Hp <= 0)
        {
            EndRun(3);
        }
    }

    private void CheckTimer()
    {
        if(m_Timer < 0)
        {
            EndRun(3);
        }
    }

    public void GetBonus(float aBonus)
    {
        if (m_BonusIsActive)
        {
            m_Timer += aBonus;
            m_BonusText.text = "+" + aBonus.ToString();
            m_BonusTextTimer = m_HitBonusTextDuration;
        }
    }
}
