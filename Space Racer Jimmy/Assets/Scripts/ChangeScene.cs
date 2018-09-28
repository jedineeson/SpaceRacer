using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
	public void ChangeLevel(string aString)
    {
        LevelManager.Instance.ChangeLevel(aString);
	}
}
