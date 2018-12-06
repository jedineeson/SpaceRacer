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

    private void Start ()
    {
        if (m_Invert)
        { 
           m_GoToPos1 = false;
           m_GoToPos2 = true;
        }
    }
	
	private void Update ()
    {
        if (transform.position.y >= 0.25f)
        {
            m_GoToPos1 = false;
            m_GoToPos2 = true;
        }
        else if (transform.position.y <= -0.25f)
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

    public void SetUpObstacles(float aMoveSpeed, bool aInvert)
    {
        m_MoveSpeed = aMoveSpeed;
        m_Invert = aInvert;
    }
}
