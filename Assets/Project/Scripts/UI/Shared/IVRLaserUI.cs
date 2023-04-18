using UnityEngine.EventSystems;
using Valve.VR.Extras;

public interface IVRButton
{
    public void OnPointerEnter(PointerEventArgs eventData);

        public void OnPointerExit(PointerEventArgs eventData);

        public void OnPointerClick(PointerEventArgs eventData);
}
