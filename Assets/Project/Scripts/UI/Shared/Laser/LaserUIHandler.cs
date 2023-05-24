
    using System;
    using UnityEngine;
    using Valve.VR;
    using Valve.VR.Extras;

    public class LaserUIHandler : MonoBehaviour
    {
        public static LaserUIHandler Instance;
        
        public LaserPointer rightLaserPointer;
        public LaserPointer leftLaserPointer;

        [SerializeReference] private bool _enabled = true;
        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                _enabled = value;
                if (_enabled)
                {
                    Activate();
                }
                else
                {
                    Deactivate();
                }
                
            }
        }
        
        private SteamVR_LaserPointer[] _laserPointers;
        
        void Awake()
        {
            Instance = this;
            
            _laserPointers = new SteamVR_LaserPointer[] {leftLaserPointer, rightLaserPointer};
            

            foreach (SteamVR_LaserPointer laserPointer in _laserPointers)
            {
                laserPointer.PointerClick += PointerClick;
                laserPointer.PointerIn += PointerEnter;
                laserPointer.PointerOut += PointerExit;
            }
            
        }

        private void Start()
        {
            if (_enabled)
            {
                Activate();
            }
            else
            {
                Deactivate();
            }
        }

        private void Update()
        {
            if (!_enabled)
            {
                return;
            }
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

        private void Deactivate()
        {
            leftLaserPointer.Deactivate();
            rightLaserPointer.Deactivate();
            leftLaserPointer.enabled = false;
            rightLaserPointer.enabled = false;
        }
        
        private void Activate()
        {
            leftLaserPointer.enabled = true;
            rightLaserPointer.enabled = true;
            leftLaserPointer.Activate();
            rightLaserPointer.Activate();
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
