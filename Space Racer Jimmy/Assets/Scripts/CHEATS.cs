using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CHEATS : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Canvas;

    private static CHEATS m_Instance;
    public static CHEATS Instance
    {
        get
        {
            return m_Instance;
        }
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
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Start ()
    {
#if UNITY_CHEAT
    Debug.Log("Debug Mode");
    m_Canvas.SetActive(true);
#else 
    m_Canvas.SetActive(false);
    gameObject.setActive(false);
#endif
    }

    private void Update ()
    {
#if UNITY_CHEAT
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (GameManager.Instance.ShipController != null)
            {
                GameManager.Instance.ShipController.SetLife(1000);
                Debug.Log("GAIN LIFE");
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (GameManager.Instance.ShipController != null)
            {
                GameManager.Instance.ShipController.EndTimerTrigger = true;
                Debug.Log("END RUN");
            }
        }
#endif
    }
}
