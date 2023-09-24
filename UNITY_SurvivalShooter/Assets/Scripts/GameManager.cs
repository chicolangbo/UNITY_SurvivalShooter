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
    public bool isPaused { get; private set; } = false;

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

    private void Update()
    {
        if (UIManager.instance.gameoverUI.GetComponent<FadeController>().isFadeInDone)
        {
            RestartGame();
        }

        if (!isGameover && Input.GetButtonDown("Cancel"))
        {
            PauseGame();
        }

        if(isPaused)
        {
            UIManager.instance.SetEnemyVolume();
        }
    }

    public void EndGame()
    {
        isGameover = true;
        UIManager.instance.OpenGameoverUI();
    }


    public void PauseGame()
    {
        if (isPaused)
        {
            isPaused = false;
            Time.timeScale = 1f;
        }
        else
        {
            isPaused = true;
            Time.timeScale = 0f;
        }
        UIManager.instance.SetActivePauseUI(isPaused);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
