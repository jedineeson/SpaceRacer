using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowAvalaibleInTheBETAVersion : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI m_TextMesh;

    private void Start()
    {
        m_TextMesh.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            m_TextMesh.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            m_TextMesh.gameObject.SetActive(false);
        }
    }
}
