using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Valve.VR;
using Valve.VR.InteractionSystem;
public class Pause : MonoBehaviour
{
    [SerializeField] GameObject pauseCanvas;
    public SteamVR_Action_Boolean pauseInput = SteamVR_Input.GetBooleanAction("Pause");
    
    public float distancePlayer = 2f;
    public float updateSpeed = 5f;

    public void Start()
    {
        pauseCanvas.SetActive(false);
    }

    public void Update()
    {
        if (pauseInput.GetStateDown(SteamVR_Input_Sources.Any) || Input.GetKeyDown(KeyCode.P))
        {
            ToogleMenu();
        }
    }

    public void ToogleMenu()
    {
        if (pauseCanvas.activeSelf)
        {
            ResumeGame();
        }
        else
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
