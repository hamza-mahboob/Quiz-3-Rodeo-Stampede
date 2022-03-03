using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Text scoreTXT;
    public GameObject gameOverScreen;
    int score;
    bool isGameOver = false;
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        scoreTXT.text = "High Score: " + score.ToString();
        if (isGameOver)
        {
            Time.timeScale = 0;
            //game over and high score UI
            gameOverScreen.SetActive(true);
        }
    }

    public void AddScore()
    {
        score += 25;
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public bool GameIsOver()
    {
        return isGameOver;
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("MainScene");
        Time.timeScale = 1;
        isGameOver = false;
    }
}
