using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private ControllerBase m_ShipController;
    public ControllerBase ShipController
    {
        get { return m_ShipController; }
        set { m_ShipController = value; }
    }

    private MakeTunnel m_TunnelGenerator;
    public MakeTunnel TunnelGenerator
    {
        get { return m_TunnelGenerator; }
        set { m_TunnelGenerator = value; }
    }

    private TimerBase m_Timer;
    public TimerBase Timer
    {
        get { return m_Timer; }
        set { m_Timer = value; }
    }

    private static GameManager m_Instance;
    public static GameManager Instance
    {
        get { return m_Instance; }
    }

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
