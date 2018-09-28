using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform m_ToFollow;
    [SerializeField]
    private float m_Distance;
    private float m_LimitX;
    private float m_LimitY;
    private static CameraFollow m_Instance;
    public static CameraFollow Instance
    {
        get { return m_Instance; }
    }

    private void Awake()
    {
        if (m_Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            m_Instance = this;
        }
    }

    void Update ()
    {
        //variable pour le z copie la position du ship
        Vector3 ToFollow = m_ToFollow.position;
        Vector3 CamPos = transform.position;

        //FAIRE UN BEAU LERP
        transform.rotation = m_ToFollow.rotation;
        CamPos.y = ToFollow.y;
        CamPos.x = ToFollow.x;

        //CamPos.y = Mathf.Lerp(CamPos.y, ToFollow.y, Time.deltaTime * m_MoveSpeed);
        //CamPos.x = Mathf.Lerp(CamPos.x, ToFollow.x, Time.deltaTime * m_MoveSpeed);

        //recule la caméro de la distance désiré
        CamPos.z = ToFollow.z - m_Distance;
        if (CamPos.x < -m_LimitX)
        {
            CamPos.x = -m_LimitX;
        }
        else if (CamPos.x > m_LimitX)
        {
            CamPos.x = m_LimitX;
        }
        if (CamPos.y < -m_LimitY)
        {
            CamPos.y = -m_LimitY;
        }
        else if (CamPos.y > m_LimitY)
        {
            CamPos.y = m_LimitY;
        }
        transform.position = CamPos;
        //Vector3 lookAt = new Vector3(0, 0, m_ToFollow.position.z);
        //transform.LookAt(lookAt);
    }

    public void SetLimit(float xLimit, float yLimit)
    {
        m_LimitX = xLimit; //-0.05f
        m_LimitY = yLimit; //0.08f
    }
}
