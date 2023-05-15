using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public Clock_System clock;
    public GameObject canvas;
    void Start()
    {
        canvas.SetActive(false);
        PlayerStats.Instance.onGameOver += gameOver;
    }

    public void gameOver()
    {
        if (PlayerStats.Instance.TimeLeft <= 0)
        {
            canvas.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
