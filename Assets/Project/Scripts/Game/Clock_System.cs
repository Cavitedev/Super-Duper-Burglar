using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        timeLeft -= Time.deltaTime;

        timeLeftText.text = timeLeft.ToString();
        moneyEarnedText.text = currentMoney.ToString()+"€";

        if (timeLeft <= 0f)
        {
            Debug.Log("SE ACABO EL TIEMPO!");
        }
    }
}
