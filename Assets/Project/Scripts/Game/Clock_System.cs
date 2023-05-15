using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Clock_System : MonoBehaviour
{

    public float totaltimeSeconds;
    public float currentMoney;

    [SerializeField] TextMeshPro timeLeftText;
    [SerializeField] TextMeshPro moneyEarnedText;

    private float timeLeft;


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

    private void Start()
    {
        timeLeft = totaltimeSeconds;
        currentMoney = 0;
    }

    void Update()
    {
        

        //timeLeftText.text = timeLeft.ToString();
        moneyEarnedText.text = currentMoney.ToString()+"€";

        if (timeLeft <= 0f)
        {
            Debug.Log("SE ACABO EL TIEMPO!");
        }
        else
        {
            timeLeft -= Time.deltaTime;

            float minutes = Mathf.FloorToInt(timeLeft / 60);
            float seconds = Mathf.FloorToInt(timeLeft % 60);
            TimeSpan time = TimeSpan.FromSeconds(timeLeft);
            timeLeftText.text = time.ToString(@"mm\:ss\:fff");
        }
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
