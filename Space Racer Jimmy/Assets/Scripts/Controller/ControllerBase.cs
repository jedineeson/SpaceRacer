﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerBase : MonoBehaviour
{
    [SerializeField]
    private TimerBase m_Timer;
    [SerializeField]
    private ShipData m_Data;
    [SerializeField]
    private TextMeshPro m_BonusText;
    [SerializeField]
    private TextMeshPro m_HitText;

    [SerializeField]
    private bool m_CanRespawn;
    [SerializeField]
    private List<Transform> m_SpawnPos;

    #region ContainsInData
    protected float m_Acceleration;
    protected float m_SlowDownSpeed;
    protected float m_BreakSpeed;
    protected float m_VelocityMax;
    protected float m_MovSpeed;
    protected float m_RotationSpeed;
    protected float m_Hp;
    protected float m_HitBonusTextDuration;
    protected Transform m_GoLeft;
    protected Transform m_GoLeftLeft;
    protected Transform m_GoLeftRight;
    protected Transform m_GoRight;
    protected Transform m_GoRightLeft;
    protected Transform m_GoRightRight;
    protected Transform m_GoStraight;
    protected Transform m_GoStraightLeft;
    protected Transform m_GoStraightRight;
    #endregion

    private Vector3 m_ShipDirection = new Vector3();

    private float m_HpMax;
    private float m_HitTextTimer;
    private float m_BonusTextTimer;
    private float m_Velocity = 0f;
    private float m_EndTimer = 3f;
    private float m_Horizontal;
    private float m_Vertical;

    private bool m_CanControl = false;
    private bool m_BonusIsActive = true;
    private bool m_Respawn = false;
    private bool m_EndTimerTrigger = false;

    private int m_ZoneReach = 0;
    private int m_ObjectivesCount = 0;

    public TimerBase Timer
    {
        get { return m_Timer; }
    }
    public float Velocity
    {
        get { return m_Velocity; }
    }
    public float HpMax
    {
        get { return m_HpMax; }
    }
    public float Hp
    {
        get { return m_Hp; }
    }
    public float HitTextTimer
    {
        get { return m_HitTextTimer; }
        set { m_HitTextTimer = value; }
    }
    public float HitBonusTextDuration
    {
        get { return m_HitBonusTextDuration; }
        set { m_HitBonusTextDuration = value; }
    }

    public bool EndTimerTrigger
    {
        get { return m_EndTimerTrigger; }
    }
    public bool CanControl
    {
        get { return m_CanControl; }
        set { m_CanControl = value; }
    }
    public bool BonusIsActive
    {
        get { return m_BonusIsActive; }
        set { m_BonusIsActive = value; }
    }

    private void Start()
    {
        #region SetValueFromData
        m_Acceleration = m_Data.Acceleration;
        m_SlowDownSpeed = m_Data.SlowDownSpeed;
        m_BreakSpeed = m_Data.BreakSpeed;
        m_VelocityMax = m_Data.VelocityMax;
        m_MovSpeed = m_Data.MovSpeed;
        m_RotationSpeed = m_Data.RotationSpeed;
        m_Hp = m_Data.Hp;
        m_HitBonusTextDuration = m_Data.HitBonusTextDuration;
        m_GoLeft = m_Data.GoLeft;
        m_GoLeftLeft = m_Data.GoLeftLeft;
        m_GoLeftRight = m_Data.GoLeftRight;
        m_GoRight = m_Data.GoRight;
        m_GoRightLeft = m_Data.GoRightLeft;
        m_GoRightRight = m_Data.GoRightRight;
        m_GoStraight = m_Data.GoStraight;
        m_GoStraightLeft = m_Data.GoStraightLeft;
        m_GoStraightRight = m_Data.GoStraightRight;
        #endregion

        m_HpMax = m_Hp;
        m_BonusText.gameObject.SetActive(false);
        m_HitText.gameObject.SetActive(false);
        GameManager.Instance.ShipController = this;
    }

    private void Update()
    {
        Move();
        ShowUIFeedBack();
        CheckEndTimer();

        if (m_Respawn)
        {
            Respawn();
        }
    }

    private void Move()
    {
        m_Horizontal = Input.GetAxis("Horizontal");
        m_Vertical = Input.GetAxis("Vertical");

        if (m_CanControl)
        {
            if (Input.GetKey(KeyCode.W) || Input.GetButton("Break"))
            {
                m_Velocity -= m_BreakSpeed * Time.deltaTime;
                if (m_Velocity < 0f)
                {
                    m_Velocity = 0f;
                }
            }

            if (Input.GetKey(KeyCode.Space) || Input.GetButton("Gaz"))
            {
                m_Velocity += m_Acceleration * Time.deltaTime;
                if (m_Velocity > m_VelocityMax)
                {
                    m_Velocity = m_VelocityMax;
                }
            }
            else
            {
                m_Velocity -= m_SlowDownSpeed * Time.deltaTime;
                if (m_Velocity < 0f)
                {
                    m_Velocity = 0f;
                }
            }

            if (Input.GetKey(KeyCode.E) || Input.GetButton("Left"))
            {

                m_ShipDirection = new Vector3(-m_Vertical, m_Horizontal, m_Velocity);
                if (m_Horizontal > 0f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_GoLeftRight.rotation, m_RotationSpeed * Time.deltaTime);
                }
                else if (m_Horizontal < 0f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_GoLeftLeft.rotation, m_RotationSpeed * Time.deltaTime);
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_GoLeft.rotation, m_RotationSpeed * Time.deltaTime);
                }
            }
            else if (Input.GetKey(KeyCode.Q) || Input.GetButton("Right"))
            {
                m_ShipDirection = new Vector3(m_Vertical, -m_Horizontal, m_Velocity);
                if (m_Horizontal > 0f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_GoRightRight.rotation, m_RotationSpeed * Time.deltaTime * 10);
                }
                else if (m_Horizontal < 0f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_GoRightLeft.rotation, m_RotationSpeed * Time.deltaTime * 10);
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_GoRight.rotation, m_RotationSpeed * Time.deltaTime);
                }
            }
            else
            {
                m_ShipDirection = new Vector3(m_Horizontal, m_Vertical, m_Velocity);
                if (m_Horizontal > 0f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_GoStraightRight.rotation, m_RotationSpeed * Time.deltaTime * 10);
                }
                else if (m_Horizontal < 0f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_GoStraightLeft.rotation, m_RotationSpeed * Time.deltaTime * 10);
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, m_RotationSpeed * Time.deltaTime);
                }
            }

            transform.position += m_ShipDirection * m_MovSpeed * Time.deltaTime;
        }
    }

    private void ShowUIFeedBack()
    {
        if (m_HitTextTimer > 0f)
        {
            m_HitText.gameObject.SetActive(true);
            m_HitTextTimer -= Time.deltaTime;
        }
        else
        {
            m_HitText.gameObject.SetActive(false);
            m_HitTextTimer = 0f;
        }
        if (m_BonusTextTimer > 0f)
        {
            m_BonusText.gameObject.SetActive(true);
            m_BonusTextTimer -= Time.deltaTime;
        }
        else
        {
            m_BonusText.gameObject.SetActive(false);
            m_BonusTextTimer = 0f;
        }
    }

    public void EndRun(int aLevel)
    {
        ScoreManager.Instance.UpdateScoreList(aLevel, m_Timer.Timer);
        m_Timer.EndTimer();
        m_EndTimerTrigger = true;
        GameManager.Instance.TunnelGenerator.ReturnStuff();
        LevelManager.Instance.ChangeLevel("Result");
    }

    public virtual void SetLife(int aGain)
    {
        m_Hp += aGain;
    }

    private void CheckEndTimer()
    {
        if (m_EndTimerTrigger)
        {
            m_EndTimer -= Time.deltaTime;     
        }
        if (m_EndTimer < 0)
        {
            EndRun(3);
        }
    }

    private void OnCollisionExit(Collision aOther)
    {
        LifeUnder0();
    }

    private void OnTriggerExit(Collider aOther)
    {
        LifeUnder0();
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Objective"))
        {
            m_ObjectivesCount += 1;
        }
    }

    private void Respawn()
    {
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

    private void LifeUnder0()
    {
        m_CanControl = false;

        if (m_Hp <= 0)
        {
            if (m_CanRespawn)
            {
                m_Respawn = true;
            }
            else
            {
                EndRun(3);
            }
        }
    }

    public void GetBonus(float aBonus, int aObjectives)
    {
        if (m_BonusIsActive)
        {
            if (m_CanRespawn && m_ObjectivesCount >= aObjectives)
            {
                m_Timer.TimeBonus(-aBonus);
                m_BonusText.text = "-" + aBonus.ToString();
            }
            else if (!m_CanRespawn)
            {
                m_Timer.TimeBonus(aBonus);
                m_BonusText.text = "+" + aBonus.ToString();
            }
            m_BonusTextTimer = m_HitBonusTextDuration;
            m_ObjectivesCount = 0;
            m_ZoneReach += 1;
            m_BonusIsActive = true;
        }
    }

    public void IncreaseObjectivesCount()
    {
        m_ObjectivesCount += 1;
    }
}
