using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class SelectALevel : MonoBehaviour
{
    [SerializeField]
    private string m_SceneToLoad;
    [SerializeField]
    private int m_Level;
    [SerializeField]
    private GameObject[] m_Stars = new GameObject[3];
    [SerializeField]
    private GameObject[] m_BlackStars = new GameObject[3];
    [SerializeField]
    private TextMeshPro m_HighScore;

    private void Start()
    {
        for (int i = 0; i <= 2; i++)
        {
            m_BlackStars[i].SetActive(false);
            m_Stars[i].SetActive(false);
        }
        m_HighScore.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (ScoreManager.Instance.Result[m_Level].StarsCount == 0)
            {
                m_Stars[0].SetActive(false);
                m_Stars[1].SetActive(false);
                m_Stars[2].SetActive(false);
            }
            else if (ScoreManager.Instance.Result[m_Level].StarsCount == 1)
            {
                m_Stars[0].SetActive(true);
                m_Stars[1].SetActive(false);
                m_Stars[2].SetActive(false);
            }
            else if (ScoreManager.Instance.Result[m_Level].StarsCount == 2)
            {
                m_Stars[0].SetActive(true);
                m_Stars[1].SetActive(true);
                m_Stars[2].SetActive(false);
            }
            else if (ScoreManager.Instance.Result[m_Level].StarsCount == 3)
            {
                m_Stars[0].SetActive(true);
                m_Stars[1].SetActive(true);
                m_Stars[2].SetActive(true);
            }

            if (ScoreManager.Instance.Result[m_Level].ScoreList.Count < 1)
            {
                m_HighScore.text = "NEW";
            }
            else
            {
                if (m_Level != 3)
                {
                    List<float> scoreList = ScoreManager.Instance.Result[m_Level].ScoreList.OrderBy(number => number).ToList();
                    m_HighScore.text = scoreList[0].ToString("f2") + " secs";
                }
                else
                {
                    List<float> scoreList = ScoreManager.Instance.Result[m_Level].ScoreList.OrderByDescending(number => number).ToList();
                    m_HighScore.text = scoreList[0].ToString("f2") + " secs";             
                }
            }

            for (int i = 0; i <= 2; i++)
            {
                m_BlackStars[i].SetActive(true);
            }
            m_HighScore.gameObject.SetActive(true);
            aOther.gameObject.GetComponent<MenuShipController>().SetSceneToLoad(m_SceneToLoad, true);
        }
    }

    private void OnTriggerExit2D(Collider2D aOther)
    {
        if (aOther.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            for (int i = 0; i <= 2; i++)
            {
                m_Stars[i].SetActive(false);
                m_BlackStars[i].SetActive(false);
            }

            m_HighScore.gameObject.SetActive(false);
            aOther.gameObject.GetComponent<MenuShipController>().SetSceneToLoad(m_SceneToLoad, false);
        }
    }
}
