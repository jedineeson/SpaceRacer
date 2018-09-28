using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneBonusEnd : MonoBehaviour
{
    [SerializeField]
    private float m_Bonus;
    [SerializeField]
    private float m_NewCamXLimit;
    [SerializeField]
    private float m_NewCamYLimit;
    [SerializeField]
    private int m_ObjectivesCount;

    private void OnTriggerExit(Collider aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            aOther.GetComponent<ShipController>().GetBonus(m_Bonus, m_ObjectivesCount);
            //Destroy(this.gameObject);
            CameraFollow.Instance.SetLimit(m_NewCamXLimit, m_NewCamYLimit);
        }
    }
}
