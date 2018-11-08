using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField]
    protected AudioClip m_HitSFX;

    private void OnCollisionEnter(Collision aOther)
    {
        HitPlayer(aOther.gameObject.GetComponent<ControllerBase>());
    }

    protected virtual void OnTriggerEnter(Collider aOther)
    {
        HitPlayer(aOther.gameObject.GetComponent<ControllerBase>());
    }

    private void HitPlayer(ControllerBase aShip)
    {
        AudioManager.Instance.PlaySFX(m_HitSFX, transform.position);
        aShip.SetLife(-1);
        aShip.BonusIsActive = false;
        aShip.HitTextTimer = aShip.HitBonusTextDuration;
    }
}
