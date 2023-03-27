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
        }
    }
}