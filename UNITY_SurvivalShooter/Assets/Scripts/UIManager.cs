using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            if(m_instacne == null)
            {
                m_instacne = FindObjectOfType<UIManager>();
            }

            return m_instacne;
        }
    }

    private static UIManager m_instacne;

    public Text scoreText;
    public GameObject gameoverUI;

    public void UpdateScoreText(int newScore)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SCORE : ");
        sb.Append("newScore");
        scoreText.text = sb.ToString();
    }

    public void SetActiveGameoverUI(bool active)
    {
        gameoverUI.SetActive(active);
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
