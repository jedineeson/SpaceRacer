using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObjectAround : MonoBehaviour
{
    [SerializeField]
    private float m_RotSpeed;
    private Vector3 m_RotateAround = new Vector3();

	private void Start ()
    {
        m_RotateAround = transform.position;
        m_RotateAround.x = 0f;
        m_RotateAround.y = 0f;
    }

    // Update is called once per frame
    void Update ()
    {
        transform.RotateAround(m_RotateAround, Vector3.forward, m_RotSpeed * Time.deltaTime);	
	}
}
