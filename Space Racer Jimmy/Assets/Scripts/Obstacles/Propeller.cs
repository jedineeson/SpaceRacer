using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{	
    [SerializeField]
    private float m_RotSpeed;

	private void Update ()
    {
        transform.Rotate(Vector3.forward, m_RotSpeed * Time.deltaTime);
	}
}