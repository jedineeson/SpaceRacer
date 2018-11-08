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


                m_Results = ScoreManager.Instance.Result[ScoreManager.Instance.Level].ScoreList.OrderBy(number => number).ToList();               
            }
            else
            {
                m_Stars[0].SetActive(true);
                m_Stars[1].SetActive(true);
                m_Stars[2].SetActive(true);
                m_Results = ScoreManager.Instance.Result[3].ScoreList.OrderByDescending(number => number).ToList();
            }
        }

        for (int i = 0; i <= m_Results.Count - 1; i++)
        {
            if (m_Results[i] < 10f && m_Results[i] > 0f)
            {
                m_ResultsText[i].text = "#" + (i + 1) + " - " + "0" + m_Results[i].ToString("f2") + " secondes";
            }
            else
            {
                m_ResultsText[i].text = "#" + (i + 1) + " - " + m_Results[i].ToString("f2") + " secondes";
            }
        }
    }
}
