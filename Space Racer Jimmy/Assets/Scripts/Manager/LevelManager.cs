using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    

    /*private int m_StarsCount;
    public int StarsCount
    {
        get { return m_StarsCount; }
    }

    private List<float> m_ScoreList = new List<float>();
    public List<float> ScoreList
    {
        get { return m_ScoreList; }
    }*/

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
            AudioManager.Instance.PlayMusic("MusicGame");
        }
        SceneManager.LoadScene(aScene);
        SceneManager.sceneLoaded += OnLoadingDone;
        
    }


}
