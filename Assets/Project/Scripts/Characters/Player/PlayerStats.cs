using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    private int _bountyAmount = 0;
    private int _bountyCount = 0;
    public int BountyCount => _bountyCount;


    private static PlayerStats _instance;
    public static PlayerStats Instance => _instance;

    private void Awake()
    {
        _instance = this;
    }

    public void AddToBounty(int bounty)
    {
        _bountyAmount += bounty;
        _bountyCount += 1;
        
        Debug.Log($"Current Bounty: <color=green>{bounty}</color>");
    }


    
}
