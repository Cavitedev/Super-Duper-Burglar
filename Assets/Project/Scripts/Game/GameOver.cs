using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public GameObject canvas;
    void Start()
    {
        canvas.SetActive(false);
        PlayerStats.Instance.onGameOver += gameOver;
    }

    public void gameOver()
    {
        Debug.Log("Game Over");
        canvas.SetActive(true);
        Time.timeScale = 0f;
    }
    
    public void Retry()
    {
        SceneManager.LoadScene("House2");
    }

}
