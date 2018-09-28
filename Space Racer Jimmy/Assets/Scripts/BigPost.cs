using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigPost : MonoBehaviour
{
    [SerializeField]
    private string m_ObstacleType;
    [SerializeField]
    private float m_MoveSpeed;
    private bool m_GoToPos1 = true;
    private bool m_GoToPos2 = false;
    private Vector3 m_BasePos;
    private Vector3 m_ToPos1;
    private Vector3 m_ToPos2;

    void Start ()
    {
        Vector3 m_BasePos = transform.position;
        if (m_ObstacleType == "Vertical")
        {
            Vector3 ToPos2 = new Vector3(90, 0, 0);
            m_ToPos1 = m_BasePos;
            m_ToPos2 = m_BasePos + ToPos2;
        }
        else if (m_ObstacleType == "Horizontal")
        {
            Vector3 ToPos2 = new Vector3(0, 90, 0);
            m_ToPos1 = m_BasePos;
            m_ToPos2 = m_BasePos + ToPos2;
        }
        
    }
	
	void Update ()
    {
        if (m_ObstacleType == "Vertical")
        {
            //pos1 reached
            if (transform.position.x <= m_ToPos1.x)
            {
                m_GoToPos1 = false;
                m_GoToPos2 = true;
            }
            //pos2 reached
            else if (transform.position.x >= m_ToPos2.x)
            {
                m_GoToPos2 = false;
                m_GoToPos1 = true;
            }
            if (m_GoToPos1)
            {
                transform.Translate(Vector3.left * Time.deltaTime * m_MoveSpeed);
            }
            else if (m_GoToPos2)
            {
                transform.Translate(Vector3.right * Time.deltaTime * m_MoveSpeed);
            }
        }
        if (m_ObstacleType == "Horizontal")
        {
            //pos1 reached
            if (transform.position.y <= m_ToPos1.y)
            {
                m_GoToPos1 = false;
                m_GoToPos2 = true;
            }
            //pos2 reached
            else if (transform.position.y >= m_ToPos2.y)
            {
                m_GoToPos2 = false;
                m_GoToPos1 = true;
            }
            if (m_GoToPos1)
            {
                transform.Translate(Vector3.down * Time.deltaTime * m_MoveSpeed);
            }
            else if (m_GoToPos2)
            {
                transform.Translate(Vector3.up * Time.deltaTime * m_MoveSpeed);
            }
        }
    }
}
