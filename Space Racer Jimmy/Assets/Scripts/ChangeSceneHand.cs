using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneHand : MonoBehaviour
{
    [SerializeField]
    private string m_SceneToLoad = "ProgressionMenu";
    private void Update()
    {
        if (Input.GetButtonDown("Gaz") || Input.GetKeyDown(KeyCode.Space))
        {
            //CHANGER POUR LA BETA
            LevelManager.Instance.ChangeLevel(m_SceneToLoad);
        }
    }
}
