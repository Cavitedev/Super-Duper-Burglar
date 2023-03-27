using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Project.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public SteamVR_Action_Vector2 moveValue;
        public float maxSpeed;
        public float sensitivity;
        public float sphereRadious = 1f;
        public float distance;
        public Rigidbody head;

        private float speed = 0.0f;

        public float crouchDistance = 0.4f;

        public SteamVR_Action_Boolean crouch = SteamVR_Input.GetBooleanAction("Crouch");
        public SteamVR_Action_Boolean standUp = SteamVR_Input.GetBooleanAction("StandUp");

        private bool isCrouched = false;

        void Update()
        {
            MovePlayer();

            bool crouchAction = crouch.GetStateDown(SteamVR_Input_Sources.RightHand);
            if (crouchAction && !isCrouched)
            {
                Crouch();
            }

            bool standUp = this.standUp.GetStateDown(SteamVR_Input_Sources.RightHand);
            if (standUp && isCrouched)
            {
                StandUp();
            }
        }

        private void MovePlayer()
        {
            if (moveValue.axis != Vector2.zero)
            {
                Vector3 direction =
                    Player.instance.hmdTransform.TransformDirection(new Vector3(moveValue.axis.x, 0,
                        moveValue.axis.y));

                speed = moveValue.axis.magnitude * sensitivity;
                speed = Mathf.Clamp(speed, 0, maxSpeed);

                RaycastHit hit;

                Vector3 point1 = transform.position;
                Vector3 point2 = transform.position;
                point1.y = 0.2f;
                point2.y = 1.8f;
                
                int mask = LayerMask.NameToLayer("Obstacles");
                
                if (Physics.CapsuleCast(point1, point2, 0.5f, direction, out hit, distance, 4))
                {
                    Debug.Log("Pared " + hit.transform.gameObject.name);
                    return;
                }
                Debug.Log("Mover");
                transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
            }
        }

        private void Crouch()
        {
            transform.position += crouchDistance * Vector3.down;
            isCrouched = true;
        }

        private void StandUp()
        {
            transform.position += crouchDistance * Vector3.up;
            isCrouched = false;
        }


    }
}