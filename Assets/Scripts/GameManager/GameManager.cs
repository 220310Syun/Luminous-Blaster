using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public static GameManager Instance;

    private Canvas gameOverCanvas, gameClearCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        gameOverCanvas = GameObject.FindWithTag("GameOverPanel").GetComponent<Canvas>();
        gameClearCanvas = GameObject.FindWithTag("GameClearPanel").GetComponent<Canvas>();
    }



    public void GameOver()
    {
        gameOverCanvas.enabled = true;

        Time.timeScale = 0f;
    }

    public void TryAgain()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(0);
    }

    void GameClear()
    {
        gameClearCanvas.enabled = true;

        Time.timeScale = 0f;
    }


    public void DelayGameClear()
    {
        Invoke("GameClear", 5f);
    }

}

