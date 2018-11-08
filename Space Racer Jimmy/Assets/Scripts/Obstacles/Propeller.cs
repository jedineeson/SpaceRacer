using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{	
    [SerializeField]
    private float m_RotSpeed;
    private bool m_RotDir = true;

	private void Update ()
    {
        if (m_RotDir == true)
        {
            transform.Rotate(Vector3.forward, m_RotSpeed * Time.deltaTime);
        }
        else
        {
            transform.Rotate(-Vector3.forward, m_RotSpeed * Time.deltaTime);
        }
    }

    public void setProp(float rotSpeed, int rotDir)
    {
        m_RotSpeed = rotSpeed;
        if (rotDir == 0)
        {
            m_RotDir = true;
        }
        else
        {
            m_RotDir = false;
        }
    }
}