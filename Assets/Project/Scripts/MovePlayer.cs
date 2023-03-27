using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Project.Scripts
{
    public class MovePlayer: MonoBehaviour
    {
        public SteamVR_Action_Vector2 moveValue;
        public float maxSpeed;
        public float sensitivity;
        public Rigidbody head;

        private float speed = 0.0f;

        public float crouchDistance = 0.4f;
        
        public SteamVR_Action_Boolean crouch = SteamVR_Input.GetBooleanAction("Crouch");
        public SteamVR_Action_Boolean standUp = SteamVR_Input.GetBooleanAction("StandUp");

        private bool isCrouched = false;
        
        void Update()
        {
         
            if (moveValue.axis != Vector2.zero )
            {
                Vector3 direction =
                    Player.instance.hmdTransform.TransformDirection(new Vector3(moveValue.axis.x, 0, moveValue.axis.y));

                speed =  moveValue.axis.magnitude * sensitivity;
                speed = Mathf.Clamp(speed, 0, maxSpeed);

                transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
            }
            
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