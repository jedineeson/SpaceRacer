using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalShipController : ControllerBase
{
    [SerializeField]
    private List<Transform> m_SpawnPos;

    private int m_ZoneReach = 0;
    private int m_ObjectivesCount = 0;

    protected override void Start()
    {
        base.Start();
        GameManager.Instance.ShipController = this;
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void UpdateTimer()
    {
        if (m_CanControl)
        {
            m_Timer += Time.deltaTime;
        }
    }

    protected override void EndRun(int aLevel)
    {
        ScoreManager.Instance.UpdateScoreList(aLevel, m_Timer);
        base.EndRun(aLevel);
    }

    private void OnCollisionExit(Collision aOther)
    {          
        if (m_Hp <= 0)
        {
            m_Respawn = true;
        }
    }

    private void OnTriggerExit(Collider aOther)
    {
        if (m_Hp <= 0)
        {
            m_Respawn = true;
        }
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Objective"))
        {
            m_ObjectivesCount += 1;
        }
    }

    private void Respawn()
    {
        m_CanControl = false;
        transform.rotation = Quaternion.Lerp(transform.rotation, m_GoStraight.rotation, m_RotationSpeed * Time.deltaTime * 10);
        transform.position = Vector3.Lerp(transform.position, m_SpawnPos[m_ZoneReach].position, Time.deltaTime);

        if (transform.position.z <= m_SpawnPos[m_ZoneReach].position.z + 1
            && (transform.position.x >= m_SpawnPos[m_ZoneReach].position.x - 0.1 && transform.position.x <= m_SpawnPos[m_ZoneReach].position.x + 0.1)
                && (transform.position.y >= m_SpawnPos[m_ZoneReach].position.y - 0.1 && transform.position.y <= m_SpawnPos[m_ZoneReach].position.y + 0.1))
        {
            m_CanControl = true;
            m_Velocity = 0f;
            m_Hp = m_HpMax;
            m_Respawn = false;
        }
    }

    public void GetBonus(float aBonus, int aObjectives)
    {
        if (m_BonusIsActive && m_ObjectivesCount >= aObjectives)
        {
            m_Timer -= aBonus;
            m_BonusText.text = "-" + aBonus.ToString();
            m_BonusTextTimer = m_HitBonusTextDuration;
        }
        m_ObjectivesCount = 0;
        m_ZoneReach += 1;
        m_BonusIsActive = true;
    }

    public void IncreaseObjectivesCount()
    {
        m_ObjectivesCount += 1;
    }
}
