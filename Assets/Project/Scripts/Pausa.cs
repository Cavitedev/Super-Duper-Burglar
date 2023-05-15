using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
public class Pausa : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;

    public float distancePlayer = 2f;
    public float updateSpeed = 5f;

    public void Start()
    {
        pauseCanvas.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetButtonDown("P"))
        {
            PauseGame();
        }
    }
    public void PauseGame()
    {
        pauseCanvas.SetActive(true);
       Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }

    public void ExitButton()
    {
        Quit();


    }

    public static void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_WEBPLAYER
         Application.OpenURL(webplayerQuitURL);
#else
         Application.Quit();
#endif
    }

}
