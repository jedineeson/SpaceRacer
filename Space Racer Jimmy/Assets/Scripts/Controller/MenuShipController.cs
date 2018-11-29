using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShipController : MonoBehaviour
{
    [SerializeField]
    private float m_MovSpeed;
    [SerializeField]
    private float m_RotSpeed;

    private float m_Horizontal;

    private string m_SceneToLoad;
    private bool m_CanChangeScene;

    public string SceneToLoad
    {
        get { return m_SceneToLoad; }
    }

    private void Update()
    {
        m_Horizontal = Input.GetAxis("Horizontal");

        if ((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.E) 
            || Input.GetButton("Break") || Input.GetButton("Left") || Input.GetButton("Right"))
            && m_CanChangeScene)
        {
            LevelManager.Instance.ChangeLevel(m_SceneToLoad);
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetButton("Gaz")))
        {
            transform.Translate(Vector3.up * m_MovSpeed * Time.deltaTime);
        }
        if ((m_Horizontal < 0 || Input.GetKey(KeyCode.LeftArrow)) && !Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward);
        }
        else if ((m_Horizontal > 0 || Input.GetKey(KeyCode.RightArrow)) && !Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(-Vector3.forward);
        }

        Mathf.Clamp(transform.position.y, -4.5f, 4.5f);
        Mathf.Clamp(transform.position.x, -8.5f, 8.5f);
    }

    public void SetSceneToLoad(string aString, bool aCanChange)
    {
        m_SceneToLoad = aString;
        m_CanChangeScene = aCanChange;
    }

}
