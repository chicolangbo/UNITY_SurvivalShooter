using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
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

    public string pauseButtonName = "Cancel";
    public bool isPaused { get; private set; } = false;

    public TextMeshProUGUI scoreText;
    public GameObject gameoverUI;
    public GameObject hitScreen;
    public GameObject pauseUI;

    public void UpdateScoreText(int newScore)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SCORE : ");
        sb.Append(newScore);
        scoreText.text = sb.ToString();
    }

    public void OpenGameoverUI()
    {
        gameoverUI.GetComponent<FadeController>().StartFade();
    }

    public void SetActivePauseUI(bool active)
    {
        pauseUI.SetActive(active);
    }

    private void Update()
    {
        isPaused = Input.GetButtonDown(pauseButtonName);
        if(isPaused)
        {
            SetActivePauseUI(isPaused);
        }
    }
}
