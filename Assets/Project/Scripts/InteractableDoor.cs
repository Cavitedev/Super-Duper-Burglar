using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent( typeof( Interactable ) )]
public class InteractableDoor : MonoBehaviour
{
    
    private bool isOpen;
    public Vector3 openRotation;
    public Vector3 closedRotation;
    public Transform ObjectToRotate;
    
// Use this for initialization
    void Start () {
        // update the current state of door
        UpdateDoorState();
    }
    void ToggleDoor()
    {
        // this will just use isOpen to toggle the door open or closed
        if(isOpen)
        {
            CloseDoor();
        } else
        {
            OpenDoor();
        }
    }
    void OpenDoor()
    {
        // set isOpen and call to update the actual door in the scene via
        // the UpdateDoorState() function
        isOpen = true;
        UpdateDoorState();
    }
    void CloseDoor()
    {
        // set isOpen and call to update the actual door in the scene via
        // the UpdateDoorState() function
        isOpen = false;
        UpdateDoorState();
    }
    void UpdateDoorState()
    {
        // here we adjust the rotation of the door so that it is physically
        // open or closed
        if(isOpen)
        {
            ObjectToRotate.localEulerAngles = openRotation;
        } else
        {
            ObjectToRotate.localEulerAngles = closedRotation;
        }
    }
    // Called every Update() while a Hand is hovering over this object
    private void HandHoverUpdate(Hand hand)
    {
        SteamVR_Input_Sources handType = SteamVR_Input_Sources.Any;
        if (SteamVR_Input.GetStateDown("GrabPinch", handType))
        {
            Debug.Log("Pinch");
            ToggleDoor();
        }
    }

}
