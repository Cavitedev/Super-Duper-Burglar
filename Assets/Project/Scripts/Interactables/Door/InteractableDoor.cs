using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : MonoBehaviour
{
    
    public bool isOpen;
    public Vector3 closedRotation;
    public AudioSource Door;

    public void ToogleDoor(Vector3 openRotation)
    {
        if(isOpen)
        {
            CloseDoor(openRotation);
        } else
        {
            OpenDoor(openRotation);
        }
    }
    void OpenDoor(Vector3 openRotation)
    {
        isOpen = true;
        UpdateDoorState(openRotation);
        Door.Play();
    }
    void CloseDoor(Vector3 openRotation)
    {
        isOpen = false;
        UpdateDoorState(openRotation);
        Door.Play();
    }
    
    void UpdateDoorState(Vector3 openRotation)
    {
        // here we adjust the rotation of the door so that it is physically
        // open or closed
        if(isOpen)
        {
            transform.localEulerAngles = openRotation;
        } else
        {
            transform.localEulerAngles = closedRotation;
        }
    }
}
