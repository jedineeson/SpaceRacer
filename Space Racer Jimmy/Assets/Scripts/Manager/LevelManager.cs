using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager m_Instance;
    public static LevelManager Instance
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
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnLoadingDone(Scene aScene, LoadSceneMode aMode)
    {
        SceneManager.sceneLoaded -= OnLoadingDone;
    }

    public void ChangeLevel(string aScene)
    {
        if(aScene == "ProgressionMenu")
        {
            AudioManager.Instance.PlayMusic("MusicMenu");
        }
        else if (aScene == "Level1")
        {
            ScoreManager.Instance.SetLevel(0);
            AudioManager.Instance.PlayMusic("MusicGame");
        }
        else if (aScene == "Survival")
        {
            ScoreManager.Instance.SetLevel(3);
            AudioManager.Instance.PlayMusic("MusicGame");
        }
        SceneManager.LoadScene(aScene);
        SceneManager.sceneLoaded += OnLoadingDone;
        
    }


}
