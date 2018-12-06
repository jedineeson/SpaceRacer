using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_VelocityText;
    [SerializeField]
    private TextMeshProUGUI m_TimerText;
    [SerializeField]
    private CanvasRenderer m_HpBar;
    [SerializeField]
    private TextMeshProUGUI m_StartCount;
    [SerializeField]
    private GameObject m_Hit;
    [SerializeField]
    private TimerBase m_Timer;
    [SerializeField]
    private ControllerBase m_Ship;
    [SerializeField]
    private AudioClip m_FirstBeep;
    [SerializeField]
    private AudioClip m_LastBeep;

    private float m_FeedBackTime = 0;

    private void Start()
    {
        GameManager.Instance.UI = this;
        m_StartCount.text = "READY";
        m_StartCount.gameObject.SetActive(false);
        StartCoroutine(BeginRace());
    }

    private void Update ()
    {
        FeedBack();       
    }

    private void FeedBack()
    {
        m_VelocityText.text = ("SPEED: " + (m_Ship.Velocity * 10).ToString("F2"));
        m_HpBar.SetColor(Color.Lerp(Color.red, Color.green, m_Ship.Hp / m_Ship.HpMax));
        if (m_Timer.Timer < 10)
        {
            m_TimerText.text = ("RUN: 0" + (m_Timer.Timer).ToString("F2"));
        }
        else
        {
            m_TimerText.text = ("RUN: " + (m_Timer.Timer).ToString("F2"));
        }

        if (m_Ship.EndTimerTrigger == true)
        {
            m_TimerText.gameObject.SetActive(false);
            m_StartCount.gameObject.SetActive(true);

            if (m_Timer.Timer < 10f && m_Timer.Timer >= 0f)
            {
                m_StartCount.text = ("RUN: 0" + (m_Timer.Timer).ToString("F2"));
            }
            else
            {
                m_StartCount.text = ("RUN: " + (m_Timer.Timer).ToString("F2"));
            }
        }
        if (m_FeedBackTime > 0f)
        {
            m_FeedBackTime -= Time.deltaTime;
        }
        else if (m_FeedBackTime < 0f)
        {
            m_FeedBackTime = 0f;
            m_StartCount.gameObject.SetActive(false);
            m_Hit.SetActive(false);
        }
    }

    private IEnumerator BeginRace()
    {
        yield return new WaitForSeconds(1f);
        m_StartCount.gameObject.SetActive(true);
        AudioManager.Instance.PlaySFX(m_FirstBeep, m_StartCount.transform.position);
        yield return new WaitForSeconds(1f);
        m_StartCount.text = "SET";
        AudioManager.Instance.PlaySFX(m_FirstBeep, m_StartCount.transform.position);

        yield return new WaitForSeconds(1f);
        m_StartCount.text = "GO!";
        AudioManager.Instance.PlaySFX(m_LastBeep, m_StartCount.transform.position);

        m_Ship.CanControl = true;
        yield return new WaitForSeconds(1f);
        m_StartCount.gameObject.SetActive(false);
    }

    public void ShowFeedBack(bool aBool, string aString)
    {
        if(aBool)
        {
            m_FeedBackTime = 1f;
            m_StartCount.text = aString;
            m_StartCount.gameObject.SetActive(true);
        }
    }

    public void ShowHitFeedBack()
    {
        m_FeedBackTime = 1f;
        m_Hit.SetActive(true);
    }
}
