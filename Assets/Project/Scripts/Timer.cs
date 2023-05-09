using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public float TimeLeft;
    public bool timerOn = false;

    //public float StartTime = 30.0f;



    public Text TimerTxt;
    //public Text BackTxt;


    void Start()
    {

        timerOn = true;

        /*if (StartTime <= 0.0f)
        {
            timerOn = true;
        }
        */

    }
    // Update is called once per frame
    void Update()
    {
        if (timerOn)
        {
            TimeLeft -= Time.deltaTime;
            updateTimer(TimeLeft);
        }

        /*if (StartTime > 0.0f)
        {
            StartTime -= Time.deltaTime;
            updateBack();
        }
        */


    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        TimerTxt.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }

    /*
     void updateBack()
    {
        //BackTxt.text = StartTime.ToString("f0");
    }
    */

}

