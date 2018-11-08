using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTunnel : MonoBehaviour
{
    private void OnTriggerExit(Collider aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.TunnelGenerator.GenerateTunnel();
        }
    }
}
