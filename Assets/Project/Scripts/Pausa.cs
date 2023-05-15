using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class Pausa : MonoBehaviour
{
    private Transform _playerCamera;
    public Transform _playerHand;
    public float distancePlayer = 2f;
    public float updateSpeed = 5f;
    public void PauseGame()
    {
       Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

  
}
