using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShipController : MonoBehaviour
{
    [SerializeField]
    private float m_MovSpeed;
    [SerializeField]
    private float m_RotSpeed;

    private Vector3 m_Vector = new Vector3();

    private float m_Horizontal;

    private string m_SceneToLoad;
    private bool m_CanChangeScene;

    public string SceneToLoad
    {
        get { return m_SceneToLoad; }
    }

    private void Update()
    {
        Move();
        Clamp();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.Space) && m_CanChangeScene)
        {
            LevelManager.Instance.ChangeLevel(m_SceneToLoad);
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.up * m_MovSpeed * Time.deltaTime);
        }
        if ((Input.GetAxis("Horizontal") < 0 || Input.GetKey(KeyCode.LeftArrow)) && !Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward);
        }
        else if ((Input.GetAxis("Horizontal") > 0 || Input.GetKey(KeyCode.RightArrow)) && !Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(-Vector3.forward);
        }
    }

    private void Clamp()
    {
        m_Vector.y = Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        m_Vector.x = Mathf.Clamp(transform.position.x, -8.5f, 8.5f);
        transform.position = m_Vector;
    }

    public void SetSceneToLoad(string aString, bool aCanChange)
    {
        m_SceneToLoad = aString;
        m_CanChangeScene = aCanChange;
    }

}
