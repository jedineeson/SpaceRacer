using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    //FAIRE DES ACTION IÇI!
    [SerializeField]
    private TextMeshProUGUI m_VelocityText;
    [SerializeField]
    private TextMeshProUGUI m_TimerText;
    [SerializeField]
    private CanvasRenderer m_HpBar;
    [SerializeField]
    private TextMeshProUGUI m_StartCount;
    [SerializeField]
    private TimerBase m_Timer;
    [SerializeField]
    private ControllerBase m_Ship;

    private void Start()
    {
        m_StartCount.text = "READY";
        m_StartCount.gameObject.SetActive(false);
        StartCoroutine(BeginRace());
    }

    private void Update ()
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
    }

    private IEnumerator BeginRace()
    {
        yield return new WaitForSeconds(1f);
        m_StartCount.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        m_StartCount.text = "SET";
        yield return new WaitForSeconds(1f);
        m_StartCount.text = "GO!";
        m_Ship.CanControl = true;
        yield return new WaitForSeconds(1f);
        m_StartCount.gameObject.SetActive(false);
    }
}
