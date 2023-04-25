using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;


public class Pickable : MonoBehaviour
{

    // Called every Update() while a Hand is hovering over this object
    private void HandHoverUpdate(Hand hand)
    {
        SteamVR_Input_Sources handType = SteamVR_Input_Sources.Any;
        if (SteamVR_Input.GetStateDown("GrabPinch", handType))
        {
            Pick();
            
            Debug.Log("Pick");
        }
    }

    private void Pick()
    {
        
        Vector3 playerPos = Player.instance.hmdTransform.transform.position;

        StartCoroutine(movePick(playerPos));

    }

    private IEnumerator movePick(Vector3 goal)
    {
        


        float goalThreshold = 0.1f;
        float speed = 0.3f;
        
        while (Vector3.SqrMagnitude(goal - transform.position) > goalThreshold)
        {
            transform.position = Vector3.MoveTowards(transform.position, goal, speed * Time.deltaTime);
            yield return null;
        }
        
        
        
        
        
    }
    
    

}
