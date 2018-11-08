using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result
{
    private int m_Level;
    public int Level
    {
        get { return m_Level; }
    }

    private int m_StarsCount = 0;
    public int StarsCount
    {
        get { return m_StarsCount; }
    }

    private List<float> m_ScoreList = new List<float>();
    public List<float> ScoreList
    {
        get { return m_ScoreList; }
    }


    public void SetLevelInt(int aLevel)
    {
        m_Level = aLevel;
    }

    public void UpdateScoreList(float aScore)
    {
        m_ScoreList.Add(aScore);
    }

    public void SetStarsCount(int aStarsCount)
    {
        if (aStarsCount > m_StarsCount)
        {
            m_StarsCount = aStarsCount;
        }
    }
}
