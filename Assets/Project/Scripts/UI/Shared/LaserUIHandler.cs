
    using UnityEngine;
    using Valve.VR.Extras;

    public class LaserUIHandler : MonoBehaviour
    {
        public SteamVR_LaserPointer[] laserPointer;

        void Awake()
        {
            foreach (SteamVR_LaserPointer laserPointer in laserPointer)
            {
                laserPointer.PointerClick += PointerClick;
                laserPointer.PointerIn += PointerEnter;
                laserPointer.PointerOut += PointerExit;
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
