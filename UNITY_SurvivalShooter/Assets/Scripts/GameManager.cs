using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance
    {
        get
        {
            if(m_instance == null)
            {
                m_instance = FindObjectOfType<GameManager>();
            }
            return m_instance;
        }
    }

    private static GameManager m_instance;
    public int score { get; private set; }
    public bool isGameover { get; private set; }

    private void Awake()
    {
        if(instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        FindObjectOfType<PlayerHp>().onDeath += EndGame;
    }

    public void AddScore(int newScore)
    {
        if(!isGameover)
        {
            score += newScore;
            UIManager.instance.UpdateScoreText(score);
        }
    }

    public void EndGame()
    {
        isGameover = true;
        UIManager.instance.OpenGameoverUI();
    }

    private void Update()
    {
        if (UIManager.instance.gameoverUI.GetComponent<FadeController>().isFadeInDone)
        {
            GameRestart();
        }
    }

    public void GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
