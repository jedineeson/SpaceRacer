using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControllerBase : MonoBehaviour
{
    [SerializeField]
    protected ShipData m_Data;
    [SerializeField]
    protected TextMeshPro m_BonusText;
    [SerializeField]
    protected TextMeshPro m_HitText;

    #region ContainsInData
    protected float m_Acceleration;
    protected float m_SlowDownSpeed;
    protected float m_BreakSpeed;
    protected float m_VelocityMax;
    protected float m_MovSpeed;
    protected float m_RotationSpeed;
    protected float m_Hp;
    protected float m_HitBonusTextDuration;
    #endregion

    protected Transform m_GoLeft;
    protected Transform m_GoLeftLeft;
    protected Transform m_GoLeftRight;
    protected Transform m_GoRight;
    protected Transform m_GoRightLeft;
    protected Transform m_GoRightRight;
    protected Transform m_GoStraight;
    protected Transform m_GoStraightLeft;
    protected Transform m_GoStraightRight;

    protected Vector3 m_ShipDirection = new Vector3();

    protected float m_Timer = 0f;
    protected float m_HpMax;
    protected float m_HitTextTimer;
    protected float m_BonusTextTimer;
    protected float m_Velocity = 0f;
    protected float m_EndTimer = 3f;
    protected float m_Horizontal;
    protected float m_Vertical;

    protected bool m_CanControl = false;
    protected bool m_BonusIsActive = true;
    protected bool m_Respawn = false;
    protected bool m_EndTimerTrigger = false;

    public float Timer
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

    protected virtual void Start()
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
    }

    protected virtual void Update()
    {
        Move();
        ShowUIFeedBack();
        UpdateTimer();
        CheckEndTimer();
    }

    protected virtual void Move()
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

    protected virtual void EndRun(int aLevel)
    {
        m_EndTimerTrigger = true;
        LevelManager.Instance.ChangeLevel("Result");
    }

    protected virtual void UpdateTimer()
    {

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

    public void EnterInTheEndTrigger()
    {
        EndRun(0);
    }
}
