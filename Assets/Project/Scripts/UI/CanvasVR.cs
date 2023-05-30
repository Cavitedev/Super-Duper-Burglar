using System.Collections.Generic;
using UnityEngine;

public class CanvasVR : MonoBehaviour
{

    public List<GameObject> deactivateOnAppearance;
    
    private void OnEnable()
    {
        LaserUIHandler.Instance.Enabled = true;
        foreach (GameObject o in deactivateOnAppearance)
        {
            o.SetActive(false);
        }
    }
    
    private void OnDisable()
    {
        LaserUIHandler.Instance.Enabled = false;
        foreach (GameObject o in deactivateOnAppearance)
        {
            o.SetActive(true);
        }
    }
}
