using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;
using Valve.VR.InteractionSystem;


[RequireComponent(typeof(Interactable))]
public class Pickable : MonoBehaviour
{

    public UnityEvent onPick;
    private AudioSource _pickau;
    
    public float pickSpeed = 1.5f;
    // Called every Update() while a Hand is hovering over this object

    private Collider _collider;


    private void Start()
    {
        _collider = GetComponent<Collider>();
        _pickau = GetComponent<AudioSource>();
    }

    private void HandHoverUpdate(Hand hand)
    {
        SteamVR_Input_Sources handType = SteamVR_Input_Sources.Any;
        if (SteamVR_Input.GetStateDown("GrabPinch", handType))
        {
            Pick();
            _pickau.Play();
            //Debug.Log("Pick");
        }
    }

    private void Pick()
    {
        

        StartCoroutine(MovePick());

    }

    private IEnumerator MovePick(){

        float overshootDist = .5f;
        float goalThreshold = 0.1f;

        Transform playerHeadTransform = Player.instance.hmdTransform.transform;
        
        Vector3 playerPos = playerHeadTransform.position;
        Vector3 dir = (playerPos - transform.position).normalized;
        Vector3 goal;
        _collider.enabled = false;
        do
        {


            
        
            goal = playerPos + dir * overshootDist + Vector3.down * 0.5f + playerHeadTransform.right * 0.3f ;
            Vector3 pos = transform.position;

            transform.position = Vector3.MoveTowards(pos, goal, pickSpeed * Time.deltaTime);

            float scaleFactor = (1 - Time.deltaTime * pickSpeed);
            transform.localScale = transform.localScale * scaleFactor;
            
            yield return null;

        } while (Vector3.SqrMagnitude(goal - transform.position) > overshootDist - goalThreshold);

        onPick?.Invoke();
        Destroy(gameObject);

    }
    
    

}
