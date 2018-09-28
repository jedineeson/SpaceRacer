using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipController : MonoBehaviour
{
    [SerializeField]
    private AudioClip m_HitSFX;

    [SerializeField]
    private ShipData m_Data;
    [SerializeField]
    private TextMeshPro m_BonusText;
    [SerializeField]
    private TextMeshPro m_HitText;
    [SerializeField]
    private List<Transform> m_SpawnPos;
    [SerializeField]
    private bool m_IsSurvival = false;

    [SerializeField]
    private Transform m_GoLeft;
    [SerializeField]
    private Transform m_GoLeftLeft;
    [SerializeField]
    private Transform m_GoLeftRight;
    [SerializeField]
    private Transform m_GoRight;
    [SerializeField]
    private Transform m_GoRightLeft;
    [SerializeField]
    private Transform m_GoRightRight;
    [SerializeField]
    private Transform m_GoStraight;
    [SerializeField]
    private Transform m_GoStraightLeft;
    [SerializeField]
    private Transform m_GoStraightRight;

    private float m_Acceleration;
    private float m_SlowDownSpeed;
    private float m_BreakSpeed;
    private float m_VelocityMax;
    private float m_MovSpeed;
    private float m_RotationSpeed;
    private float m_Hp;
    private float m_HitBonusTextDuration;
    private float m_NitroTime;

    private int m_ZoneReach = 0;
    private int m_ObjectivesCount = 0;

    private float m_Timer;
    private float m_SurvivalTimer = 0;
    private float m_Velocity = 0;
    private float m_AccelerationCopy;
    private float m_HitTextTimer;
    private float m_BonusTextTimer;
    private float m_NitroTimer;
    private float m_EndTimer = 3f;
    private float m_HpMax;
    //True pour debug seulement
    private bool m_CanControl = true;
    private bool m_BonusIsActive = true;
    private bool m_EndTimerTrigger = false;
    private bool m_IsRunnin = true;

    public bool EndTimerTrigger
    {
        get { return m_EndTimerTrigger; }
    }
    public float Hp
    {
        get { return m_Hp; }
    }
    public float HpMax
    {
        get { return m_HpMax; }
    }
    public float Timer
    {
        get { return m_Timer; }
    }
    public float Velocity
    {
        get { return m_Velocity; }
    }

    private void Awake()
    {
        m_Acceleration = m_Data.Acceleration;
        m_SlowDownSpeed = m_Data.SlowDownSpeed;
        m_BreakSpeed = m_Data.BreakSpeed;
        m_VelocityMax = m_Data.VelocityMax;
        m_MovSpeed = m_Data.MovSpeed;
        m_RotationSpeed = m_Data.RotationSpeed;
        m_Hp = m_Data.Hp;
        m_HitBonusTextDuration = m_Data.HitBonusTextDuration;
        m_NitroTime = m_Data.NitroTime;
        if(m_IsSurvival)
        {
            m_HitText.text = "+1s";
            m_Timer = 15f;
        }
    }

    private void Start()
    {
        m_AccelerationCopy = m_Acceleration;
        m_BonusText.gameObject.SetActive(false);
        m_HitText.gameObject.SetActive(false);
        m_HpMax = m_Hp;

        CameraFollow.Instance.SetLimit(0.05f, 0.05f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            EndRun(0);
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            vertical = 1;
        }
        else if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            vertical = -1;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow))
        {
            horizontal = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
        {
            horizontal = 1;
        }

        Vector3 shipDirection;

        if (m_EndTimerTrigger)
        {
            m_EndTimer -= Time.deltaTime;
        }
        if (m_EndTimer < 0)
        {
            MakeTunnel.Instance.ReturnStuff();
            LevelManager.Instance.ChangeLevel("Result");
        }
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

        if (m_NitroTimer > 0)
        {
            m_Acceleration = m_AccelerationCopy * 3;
            m_NitroTimer -= Time.deltaTime;
        }
        else
        {
            m_Acceleration = m_AccelerationCopy;
        }


        if (m_CanControl)
        {
            if (m_IsRunnin)
            {
                if (m_IsSurvival)
                {
                    m_Timer -= Time.deltaTime;
                    m_SurvivalTimer += Time.deltaTime;
                }
                else
                {
                    m_Timer += Time.deltaTime;
                }
            }

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

                shipDirection = new Vector3(-vertical, horizontal, m_Velocity);
                if (horizontal > 0f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_GoLeftRight.rotation, m_RotationSpeed * Time.deltaTime);
                }
                else if (horizontal < 0f)
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
                shipDirection = new Vector3(vertical, -horizontal, m_Velocity);
                if (horizontal > 0f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_GoRightRight.rotation, m_RotationSpeed * Time.deltaTime * 10);
                }
                else if (horizontal < 0f)
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
                shipDirection = new Vector3(horizontal, vertical, m_Velocity);
                if (horizontal > 0f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_GoStraightRight.rotation, m_RotationSpeed * Time.deltaTime * 10);
                }
                else if (horizontal < 0f)
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, m_GoStraightLeft.rotation, m_RotationSpeed * Time.deltaTime * 10);
                }
                else
                {
                    transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.identity, m_RotationSpeed * Time.deltaTime);
                }
            }

            transform.position += shipDirection * m_MovSpeed * Time.deltaTime;
        }
    }

    private void Respawn()
    {
        m_CanControl = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.rotation = Quaternion.Lerp(transform.rotation, m_GoStraight.rotation, m_RotationSpeed * Time.deltaTime * 10);
        transform.position = Vector3.Lerp(transform.position, m_SpawnPos[m_ZoneReach].position, Time.deltaTime);

        if (transform.position.z <= m_SpawnPos[m_ZoneReach].position.z + 1 
            && (transform.position.x >= m_SpawnPos[m_ZoneReach].position.x - 0.1 && transform.position.x <= m_SpawnPos[m_ZoneReach].position.x + 0.1)
                && (transform.position.y >= m_SpawnPos[m_ZoneReach].position.y - 0.1 && transform.position.y <= m_SpawnPos[m_ZoneReach].position.y + 0.1))
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            m_CanControl = true;
            m_Velocity = 0f;
            m_Hp = m_HpMax;
        }
    }

    private void OnCollisionEnter(Collision aOther)
    {
        if (m_CanControl)
        {
            if (aOther.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                AudioManager.Instance.PlaySFX(m_HitSFX, transform.position);
                m_Hp -= 1f;
                if (!m_IsSurvival && m_Hp <= 0)
                {
                    Respawn();              
                }
                else if(m_Hp <= 0)
                {
                    EndRun(3);
                }
                m_BonusIsActive = false;
                m_HitTextTimer = m_HitBonusTextDuration;
            }


            /*if (aOther.gameObject.layer == LayerMask.NameToLayer("Obstacle") || aOther.gameObject.layer == LayerMask.NameToLayer("Wall"))
            {
                m_BonusIsActive = false;
                m_Hp -= 1f;
                m_HitTextTimer = m_HitBonusTextDuration;
            }
            if (aOther.gameObject.layer == LayerMask.NameToLayer("Objective"))
            {
                m_ObjectivesCount += 1;
            }*/
        }
    }

    private void OnTriggerEnter(Collider aOther)
    {
        if (m_CanControl)
        {
            if (aOther.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
            {
                AudioManager.Instance.PlaySFX(m_HitSFX, transform.position);
                m_BonusIsActive = false;
                m_Hp -= 1f;
                if (m_Hp <= 0f)
                {
                    if (!m_IsSurvival)
                    {
                        Respawn();
                    }
                    else
                    {
                        EndRun(3);
                    }
                }
                m_HitTextTimer = m_HitBonusTextDuration;
            }
            if (aOther.gameObject.layer == LayerMask.NameToLayer("Objective"))
            {
                m_ObjectivesCount += 1;
            }

        }
    }

    public void SetCanControl()
    {
        m_CanControl = true;
    }

    public void GetBonus(float aBonus, int aObjectives)
    {
        if (!m_IsSurvival)
        {
            if (m_BonusIsActive && m_ObjectivesCount >= aObjectives)
            {
                m_Timer -= aBonus;
                m_BonusText.text = "-" + aBonus.ToString();
            }
        }
        else
        {
            if (m_BonusIsActive)
            {
                m_Timer += aBonus;
                m_BonusText.text = "+" + aBonus.ToString();           
            }
        }

        m_BonusTextTimer = m_HitBonusTextDuration;
        m_BonusIsActive = true;
        m_ObjectivesCount = 0;
        m_ZoneReach += 1;

    }

    public void IncreaseObjectivesCount()
    {
        m_ObjectivesCount += 1;
    }

    /*public void SpeedBoost()
    {
        m_NitroTimer += m_NitroTime;
        m_VelocityMax += 50;
    }*/

    public void EndRun(int aLevel)
    {
        m_IsRunnin = false;
        m_EndTimerTrigger = true;
        if (aLevel != 3)
        {
            ScoreManager.Instance.UpdateScoreList(aLevel, m_Timer);
        }
        else
        {
            ScoreManager.Instance.UpdateScoreList(3, m_SurvivalTimer);
        }     
    }
}