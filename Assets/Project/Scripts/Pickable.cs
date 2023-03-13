using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class Pickable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Called every Update() while a Hand is hovering over this object
    private void HandHoverUpdate(Hand hand)
    {
        SteamVR_Input_Sources handType = SteamVR_Input_Sources.Any;
        if (SteamVR_Input.GetStateDown("GrabPinch", handType))
        {
            
            
            Debug.Log("Pick");
        }
    }

}
