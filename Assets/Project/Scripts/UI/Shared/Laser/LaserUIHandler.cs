
    using System;
    using UnityEngine;
    using Valve.VR;
    using Valve.VR.Extras;

    public class LaserUIHandler : MonoBehaviour
    {
        public LaserPointer rightLaserPointer;
        public LaserPointer leftLaserPointer;


        private SteamVR_LaserPointer[] _laserPointers;
        
        void Awake()
        {
            _laserPointers = new SteamVR_LaserPointer[] {leftLaserPointer, rightLaserPointer};
            

            foreach (SteamVR_LaserPointer laserPointer in _laserPointers)
            {
                laserPointer.PointerClick += PointerClick;
                laserPointer.PointerIn += PointerEnter;
                laserPointer.PointerOut += PointerExit;
            }
            
        }

        private void Update()
        {
            UpdateActiveController();
        }

        private void UpdateActiveController()
        {
            if (leftLaserPointer.interactWithUI.GetStateDown(SteamVR_Input_Sources.LeftHand))
            {
                leftLaserPointer.enabled = true;
                leftLaserPointer.Activate();
                rightLaserPointer.Deactivate();
                rightLaserPointer.enabled = false;

                // leftLaserPointer.active = true;
                // rightLaserPointer.active = false;
            }else if (leftLaserPointer.interactWithUI.GetStateDown(SteamVR_Input_Sources.RightHand))
            {
                rightLaserPointer.enabled = true;
                rightLaserPointer.Activate();
                leftLaserPointer.Deactivate();
                leftLaserPointer.enabled = false;
            }
        }

        public void PointerClick(object sender, PointerEventArgs e)
        {
            IVRButton vrButton = e.target.GetComponent<IVRButton>();
            

            if (vrButton != null)
            {
                vrButton.OnPointerClick(e);
            }
        }
        
        public void PointerEnter(object sender, PointerEventArgs e)
        {
            IVRButton vrButton = e.target.GetComponent<IVRButton>();
            

            if (vrButton != null)
            {
                vrButton.OnPointerEnter(e);
            }
        }
        
        public void PointerExit(object sender, PointerEventArgs e)
        {
            IVRButton vrButton = e.target.GetComponent<IVRButton>();
            

            if (vrButton != null)
            {
                vrButton.OnPointerExit(e);
            }
        }
    }
