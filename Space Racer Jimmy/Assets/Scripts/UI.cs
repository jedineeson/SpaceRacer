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
    private ShipController m_ShipController;

    private void Start()
    {
        m_StartCount.text = "READY";
        m_StartCount.gameObject.SetActive(false);
        StartCoroutine(BeginRace());
    }

    private void Update ()
    {
        m_VelocityText.text = ("SPEED: " + (m_ShipController.Velocity * 10).ToString("F2"));
        m_HpBar.SetColor(Color.Lerp(Color.red, Color.green, m_ShipController.Hp / m_ShipController.HpMax));
        if (m_ShipController.Timer < 10)
        {
            m_TimerText.text = ("RUN: 0" + (m_ShipController.Timer).ToString("F2"));
        }
        else
        {
            m_TimerText.text = ("RUN: " + (m_ShipController.Timer).ToString("F2"));
        }

        if (m_ShipController.EndTimerTrigger == true)
        {
            m_TimerText.gameObject.SetActive(false);
            m_StartCount.gameObject.SetActive(true);

            if (m_ShipController.Timer < 10f && m_ShipController.Timer >= 0f)
            {
                m_StartCount.text = ("RUN: 0" + (m_ShipController.Timer).ToString("F2"));
            }
            else
            {
                m_StartCount.text = ("RUN: " + (m_ShipController.Timer).ToString("F2"));
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
        m_ShipController.SetCanControl();
        yield return new WaitForSeconds(1f);
        m_StartCount.gameObject.SetActive(false);
    }
}
