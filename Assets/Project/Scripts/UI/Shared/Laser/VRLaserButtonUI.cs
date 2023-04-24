
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Valve.VR.Extras;

[RequireComponent(typeof(Button))]
    public class VRLaserButtonUI : VRLaserUI
    {
        private Button _button;

        private void Start()
        {
            _button = GetComponent<Button>();
        }

        public override void OnPointerClick(PointerEventArgs eventData)
        {
            base.OnPointerClick(eventData);
            _button.onClick.Invoke();
        }

        public override void OnPointerEnter(PointerEventArgs eventData)
        {
            base.OnPointerEnter(eventData);
            _button.Select();
        }

        public override void OnPointerExit(PointerEventArgs eventData)
        {
            base.OnPointerExit(eventData);
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
