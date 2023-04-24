using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent( typeof( Interactable ) )]
public class InteractableHandle : MonoBehaviour
{
    public InteractableDoor interactableDoor;

    public Vector3 openRotation;
    
    void ToggleDoor()
    {
        interactableDoor.ToogleDoor(openRotation);
       
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
