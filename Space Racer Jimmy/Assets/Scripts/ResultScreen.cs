using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ResultScreen : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> m_ResultsText;
    [SerializeField]
    private List<GameObject> m_Stars;
    [SerializeField]
    private TextMeshProUGUI m_FinalResult;
    private List<float> m_Results = new List<float>();

    void Start()
    {
        if (ScoreManager.Instance != null)
        {
            if (ScoreManager.Instance.LastResult < 10f && ScoreManager.Instance.LastResult > 10f)
            {
                m_FinalResult.text = "0" + ScoreManager.Instance.LastResult.ToString("f2") + " secondes";
            }
            else
            {
                m_FinalResult.text = ScoreManager.Instance.LastResult.ToString("f2") + " secondes";
            }

            ScoreManager.Instance.AddScore(ScoreManager.Instance.LastResult);

            if (ScoreManager.Instance.Level != 3)
            {
                switch (ScoreManager.Instance.LastStarsCount)
                {
                    case 0:
                        m_Stars[0].SetActive(false);
                        m_Stars[1].SetActive(false);
                        m_Stars[2].SetActive(false);
                        break;
                    case 1:
                        m_Stars[0].SetActive(true);
                        m_Stars[1].SetActive(false);
                        m_Stars[2].SetActive(false);
                        break;
                    case 2:
                        m_Stars[0].SetActive(true);
                        m_Stars[1].SetActive(true);
                        m_Stars[2].SetActive(false);
                        break;
                    case 3:
                        m_Stars[0].SetActive(true);
                        m_Stars[1].SetActive(true);
                        m_Stars[2].SetActive(true);
                        break;
                }


                List<float> scoreList = ScoreManager.Instance.Results.OrderBy(number => number).ToList();
                ScoreManager.Instance.UpdateScores(scoreList);
            }
            else
            {
                List<float> scoreList = ScoreManager.Instance.Results.OrderByDescending(number => number).ToList();
                ScoreManager.Instance.UpdateScores(scoreList);
            }
        }

        /*
        for (int j = 0; j <= m_ResultsText.Count - 1; j++)
        {
            float temp = 0;
            int index = 0;

            for (int i = 0; i <= ScoreManager.Instance.Results.Count - 1; i++)
            {
                if (i == 0 || ScoreManager.Instance.Results[i] < temp)
                {
                    temp = ScoreManager.Instance.Results[i];
                    index = i;
                }
            }

            scoreList.Add(temp);
            ScoreManager.Instance.RemoveScoreFromList(index);
        }
        */

        for (int i = 0; i <= ScoreManager.Instance.Results.Count - 1; i++)
        {
            if (ScoreManager.Instance.Results[i] < 10f && ScoreManager.Instance.Results[i] > 0f)
            {
                m_ResultsText[i].text = "#" + (i + 1) + " - " + "0" + ScoreManager.Instance.Results[i].ToString("f2") + " secondes";
            }
            else
            {
                m_ResultsText[i].text = "#" + (i + 1) + " - " + ScoreManager.Instance.Results[i].ToString("f2") + " secondes";
            }
        }

        /*for (int i = 0; i <= m_Results.Count; i++)
        {
            if (scoreList.Count >= i)
            {
                m_Results.Add(scoreList[i]);

                if (m_Results[i] < 10f && m_Results[i] > 0f)
                {
                    m_ResultsText[i].text = "#" + (i + 1) + " - " + "0" + m_Results[i].ToString("f2") + " secondes";
                }
                else
                {
                    m_ResultsText[i].text = "#" + (i + 1) + " - " + m_Results[i].ToString("f2") + " secondes";
                }
            }
            else
            {
                return;
            }

        }*/
    }
}

/* if(minutes< 10) {
     minutes = "0" + minutes.ToString();
 }
 if(seconds< 10) {
     seconds = "0" + Mathf.RoundToInt(seconds).ToString();
 }*/
