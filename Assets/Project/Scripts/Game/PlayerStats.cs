using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private bool timeActive = true;

    public delegate void OnGameOver();

    public OnGameOver onGameOver;
    private bool _isGameOver;

    // TIME
    private float _timeLeft;
    public float timeSpeedMulty = 1f;
    public float totalTime;
    public float TimeLeft
    {
        get { return _timeLeft; }
        set
        {
            if (!timeActive) return;
            _timeLeft = value;
            Clock_System.Instance.UpdateTime(_timeLeft);
        }
    }

    // MONEY
    private int _bountyAmount = 100;
    public int BountyAmount
    {
        get { return _bountyAmount; }
        set
        {
            _bountyAmount = value;
            Clock_System.Instance.UpdateMoney(_bountyAmount);
        }
    }

    private int _bountyCount = 0;
    public int BountyCount => _bountyCount;


    private static PlayerStats _instance;
    public static PlayerStats Instance => _instance;

    private void Start()
    {
        //Update UI
        TimeLeft = totalTime;
        BountyAmount = _bountyAmount;
    }

    private void Awake()
    {
        _instance = this;
    }


    private void Update()
    {
        if (TimeLeft <= 0f)
        {
            TimeLeft = 0;
            if (!_isGameOver)
            {
                onGameOver?.Invoke();
                _isGameOver = true;
            }
        }
        else
        {
            TimeLeft -= Time.deltaTime * timeSpeedMulty;
        }
    }

    public void IncreaseTimeSpeed()
    {
        timeSpeedMulty = 2f;
    }

    public void DecreaseTimeSpeed()
    {
        timeSpeedMulty = 1f;
    }

    public void addToTimeMultiplier()
    {
        StartCoroutine(timeGoesFaster());
    }

    public IEnumerator timeGoesFaster()
    {
        timeSpeedMulty = 1.3f;
        yield return new WaitForSeconds(8f);
        timeSpeedMulty = 1f;
    }

    public void AddToBounty(int bounty)
    {
        BountyAmount += bounty;
        _bountyCount += 1;


        Debug.Log($"Current Bounty: <color=green>{bounty}</color>");
    }

    public void GameOver()
    {
        onGameOver();
    }
}