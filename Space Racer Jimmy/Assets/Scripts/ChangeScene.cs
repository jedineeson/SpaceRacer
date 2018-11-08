using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
	public void ChangeLevel(string aString)
    {
        LevelManager.Instance.ChangeLevel(aString);
    }

    public void PlayAgain()
    {
        if(ScoreManager.Instance.Level == 0)
        {
            LevelManager.Instance.ChangeLevel("Level1");
        }
        if (ScoreManager.Instance.Level == 3)
        {
            LevelManager.Instance.ChangeLevel("Survival");
        }
    }
}
