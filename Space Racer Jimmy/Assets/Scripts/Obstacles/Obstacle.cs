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

    private void OnTriggerEnter(Collider aOther)
    {
        HitPlayer(aOther.gameObject.GetComponent<ControllerBase>());
    }

    private void HitPlayer(ControllerBase aShip)
    {
        AudioManager.Instance.PlaySFX(m_HitSFX, transform.position);
        aShip.SetLife(-1);
        GameManager.Instance.UI.ShowHitFeedBack();
        aShip.BonusIsActive = false;
    }
}
