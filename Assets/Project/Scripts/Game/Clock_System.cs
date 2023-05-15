using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Clock_System : MonoBehaviour
{




    [SerializeField] TextMeshPro timeLeftText;
    [SerializeField] TextMeshPro moneyEarnedText;
    

    private static Clock_System _instance;

    public static Clock_System Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance != null && _instance != this)
        { //Singleton
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }



    public void UpdateTime(float timeLeft)
    {
        float minutes = Mathf.FloorToInt(timeLeft / 60);
        float seconds = Mathf.FloorToInt(timeLeft % 60);
        TimeSpan time = TimeSpan.FromSeconds(timeLeft);
        timeLeftText.text = time.ToString(@"mm\:ss\:fff");
    }

    public void UpdateMoney(int currentMoney)
    {
        moneyEarnedText.text = $"{currentMoney}€";
    }

    /*
     void Update()
    {
        if (timerOn)
        {
            TimeUsed += Time.deltaTime;
            updateTimer(TimeUsed);
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        TimeSpan time = TimeSpan.FromSeconds(TimeUsed);
        TimerTxt.text = time.ToString(@"mm\:ss\:fff");//string.Format("{0:00} : {1:00}", minutes, seconds);
    }
     */
}
