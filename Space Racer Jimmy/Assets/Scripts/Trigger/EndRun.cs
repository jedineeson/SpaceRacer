using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRun : MonoBehaviour
{
    [SerializeField]
    private int m_Level;
    [SerializeField]
    private float m_Gold;
    [SerializeField]
    private float m_Silver;
    [SerializeField]
    private float m_Bronze;

    private void OnTriggerEnter(Collider aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (aOther.gameObject.GetComponent<ShipController>().Timer <= m_Gold)
            {
                ScoreManager.Instance.SetStarsCount(m_Level, 3);
            }
            else if (aOther.gameObject.GetComponent<ShipController>().Timer <= m_Silver)
            {
                ScoreManager.Instance.SetStarsCount(m_Level, 2);
            }
            else if (aOther.gameObject.GetComponent<ShipController>().Timer <= m_Bronze)
            {
                ScoreManager.Instance.SetStarsCount(m_Level, 1);
            }
            else
            {
                ScoreManager.Instance.SetStarsCount(m_Level, 0);
            }
            aOther.gameObject.GetComponent<ShipController>().EndRun(m_Level);
        }
    }
}
