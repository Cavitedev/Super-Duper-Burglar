using UnityEngine;

public class CanvasVR : MonoBehaviour
{
    private void OnEnable()
    {
        LaserUIHandler.Instance.Enabled = true;
    }
    
    private void OnDisable()
    {
        LaserUIHandler.Instance.Enabled = false;
    }
}
