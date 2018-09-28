using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppLauncher : MonoBehaviour
{
    void Start ()
    {
        LevelManager.Instance.ChangeLevel("Menu");
	}
	
}
