using System;
using UnityEngine;
using Valve.VR;

public class FlashlightInput : MonoBehaviour
    {
        private Light _light;
        public SteamVR_Action_Boolean flashlight = SteamVR_Input.GetBooleanAction("Flashlight");


        private void Start()
        {
            _light = GetComponent<Light>();
            
        }

        private void Update()
        {
            if (flashlight.GetStateDown(SteamVR_Input_Sources.Any) || Input.GetKeyDown(KeyCode.F))
            {
                ToggleFlashlight();
            }
        }

        private void ToggleFlashlight()
        {
            _light.enabled = !_light.enabled;
        }
    }
