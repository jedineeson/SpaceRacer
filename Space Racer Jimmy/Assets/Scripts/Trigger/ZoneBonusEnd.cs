using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneBonusEnd : MonoBehaviour
{
    [SerializeField]
    private float m_Bonus;
    [SerializeField]
    private int m_ObjectivesCount;

    private void OnTriggerExit(Collider aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (aOther.GetComponent<NormalShipController>())
            {
                aOther.GetComponent<NormalShipController>().GetBonus(m_Bonus, m_ObjectivesCount);
            }
            else if (aOther.GetComponent<SurvivalShipController>())
            {
                aOther.GetComponent<SurvivalShipController>().GetBonus(m_Bonus);
            }
        }
    }
}
