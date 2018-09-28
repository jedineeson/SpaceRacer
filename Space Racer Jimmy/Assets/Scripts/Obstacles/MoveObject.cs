using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField]
    private float m_MoveSpeed;
    [SerializeField]
    private bool m_Invert = false;
    private bool m_GoToPos1 = true;
    private bool m_GoToPos2 = false;
    private Vector3 m_ToPos1;
    private Vector3 m_ToPos2;

    private void Start ()
    {
        Vector3 scaleY = transform.localScale;
        scaleY.x = 0f;
        scaleY.z = 0f;
        scaleY.y *= 0.25f;
        m_ToPos1 = transform.position + scaleY;
        m_ToPos2 = transform.position - scaleY;

        if (m_Invert)
        { 
           m_GoToPos1 = false;
           m_GoToPos2 = true;
        }
    }
	
	void Update ()
    {
        if (transform.position.y >= m_ToPos1.y)
        {
            m_GoToPos1 = false;
            m_GoToPos2 = true;
        }
        else if (transform.position.y <= m_ToPos2.y)
        {
            m_GoToPos2 = false;
            m_GoToPos1 = true;
        }
        if (m_GoToPos2)
        {
            transform.Translate(Vector3.down * Time.deltaTime * m_MoveSpeed);
        }
        else if (m_GoToPos1)
        {
            transform.Translate(Vector3.up * Time.deltaTime * m_MoveSpeed);
        }
    }
}
