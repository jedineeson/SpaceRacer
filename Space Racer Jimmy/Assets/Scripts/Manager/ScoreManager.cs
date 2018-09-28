using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{

    private int m_Level;
    public int Level
    {
        get { return m_Level; }
    }

    private float m_LastResult;
    public float LastResult
    {
        get { return m_LastResult; }
    }

    private int m_LastStarsCount;
    public int LastStarsCount
    {
        get { return m_LastStarsCount; }
    }

    private List<float> m_Results = new List<float>();
    public List<float> Results
    {
        get { return m_Results; }
    }

    private Result[] m_Result = new Result[4];
    public Result[] Result
    {
        get { return m_Result; }
    }

    private static ScoreManager m_Instance;
    public static ScoreManager Instance
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

    private void Start ()
    {
        m_Result[0] = new Result();
        m_Result[1] = new Result();
        m_Result[2] = new Result();
        m_Result[3] = new Result();
    }

    public void UpdateScoreList(int aLevel, float aScore)
    {
        m_Result[aLevel].UpdateScoreList(aScore);
        m_LastResult = aScore;
        m_Level = aLevel;
    }

    public void SetStarsCount(int aLevel, int aStarsCount)
    {
        m_Result[aLevel].SetStarsCount(aStarsCount);
        m_LastStarsCount = aStarsCount;
    }

    public void AddScore(float score)
    {
        m_Results.Add(score);
    }

    public void UpdateScores(List<float> results)
    {
        m_Results = results;
    }

    public void RemoveScoreFromList(int index)
    {
        m_Results.RemoveAt(index);
    }

}
