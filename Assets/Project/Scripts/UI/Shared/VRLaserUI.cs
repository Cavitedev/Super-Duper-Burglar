
    using UnityEngine;
    using UnityEngine.Events;
    using Valve.VR.Extras;

    public class VRLaserUI : MonoBehaviour, IVRButton
    {
        public UnityEvent onClick;
        public UnityEvent onEnter;
        public UnityEvent onExit;



        public virtual void OnPointerEnter(PointerEventArgs eventData)
        {
            onEnter?.Invoke();
        }

        public virtual void OnPointerExit(PointerEventArgs eventData)
        {
            onExit?.Invoke();
        }

        public virtual void OnPointerClick(PointerEventArgs eventData)
        {
            onClick?.Invoke();
        }
    }
