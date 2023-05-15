using UnityEngine;

public class CanvasVR : MonoBehaviour
{
    private void OnEnable()
    {
        LaserUIHandler.Instance.enabled = true;
    }
    
    private void OnDisable()
    {
        LaserUIHandler.Instance.enabled = false;
    }
}
