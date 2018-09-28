using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShipController : MonoBehaviour
{
    [SerializeField]
    private float m_MovSpeed;
    [SerializeField]
    private float m_RotSpeed;

    private string m_SceneToLoad;
    private bool m_CanChangeScene;

    public string SceneToLoad
    {
        get { return m_SceneToLoad; }
    }

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if ((Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.E) 
            || Input.GetButton("Break") || Input.GetButton("Left") || Input.GetButton("Right"))
            && m_CanChangeScene)
        {
            LevelManager.Instance.ChangeLevel(m_SceneToLoad);
        }

        if(Input.GetKeyDown(KeyCode.P))
        {
            LevelManager.Instance.ChangeLevel("ProceduralShit");
        }

        if ((Input.GetKey(KeyCode.Space) || Input.GetButton("Gaz")))
        {
            transform.Translate(Vector3.up * m_MovSpeed * Time.deltaTime);
        }
        if ((horizontal < 0 || Input.GetKey(KeyCode.LeftArrow)) && !Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.forward);
        }
        else if ((horizontal > 0 || Input.GetKey(KeyCode.RightArrow)) && !Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(-Vector3.forward);
        }

        /*Vector3 shipDirection;
        shipDirection = new Vector3(horizontal, vertical);
        Vector3 shipRot = new Vector3();
        shipRot = transform.position + shipDirection;*/

        //Vector3 shipDirection = new Vector3(horizontal, vertical, 0f);

        /*Vector3 shipDirection = (transform.position + shipDirection3D) - transform.position;

        //sqrMagnitude???
        if (shipDirection.sqrMagnitude > 0.0f)
        {
            transform.rotation = Quaternion.LookRotation(shipDirection, Vector3.forward);
        }*/

        //transform.Translate(shipDirection * m_MovSpeed * Time.deltaTime);

        //Quaternion shipRot = new Quaternion();
        //shipRot.eulerAngles = new Vector3(0, 0, -(Mathf.Atan2(horizontal, vertical) * Mathf.Rad2Deg));

        //float rotAngle = -Mathf.Atan2(vertical, horizontal) * Mathf.Rad2Deg;
        //transform.eulerAngles = new Vector3(0, 0, rotAngle);

        // and afterward, if you want to constrain the rotation to a particular axis- in this case Y:*/
        //Vector3 shipRotation;
        //shipRotation = new Vector3(horizontal, vertical, 0f);



        //transform.Rotate(Vector3.forward);
        /*
        Quaternion rotQuarternion;
        rotQuarternion = Quaternion.Euler(shipRotation);
        transform.rotation = rotQuarternion;*/
    }

    public void SetSceneToLoad(string aString, bool aCanChange)
    {
        m_SceneToLoad = aString;
        m_CanChangeScene = aCanChange;
    }

}
