using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR.InteractionSystem;
using UnityEngine.UI;
using TMPro;

public class GameOver : MonoBehaviour
{
    public GameObject canvas;

    public TextMeshPro score;

    [SerializeField] public PlayerStats amount;

    void Start()
    {
        canvas.SetActive(false);

        if (PlayerStats.Instance != null)
        {
            PlayerStats.Instance.onGameOver += gameOver;
        }
    }

    public void gameOver()
    {
        canvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Retry()
    {
        SceneManager.LoadScene("House2");
    }
}