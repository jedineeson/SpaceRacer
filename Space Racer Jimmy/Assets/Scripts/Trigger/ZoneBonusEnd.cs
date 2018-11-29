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

    private void OnTriggerEnter(Collider aOther)
    {
        Debug.Log(aOther.gameObject.name);
        aOther.GetComponent<ControllerBase>().GetBonus(m_Bonus, m_ObjectivesCount);
    }
}
