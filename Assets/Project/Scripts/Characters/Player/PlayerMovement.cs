
using UnityEngine;

using UnityEngine.Serialization;
using Valve.VR;
using Valve.VR.InteractionSystem;

namespace Project.Scripts
{
    public class PlayerMovement : MonoBehaviour
    {
        public Transform playerTransform;

        public SteamVR_Action_Vector2 moveValue;
        public float maxSpeed;
        public float sensitivity;
        [FormerlySerializedAs("sphereRadious")] public float radious = 1f;
        public float distance;
        public Rigidbody head;
        public float height = 2f;
        public float crouchDistance = 0.4f;

        public AudioSource footsteps;
        public float delay = 0.5f;


        public SteamVR_Action_Boolean crouch = SteamVR_Input.GetBooleanAction("Crouch");
        public SteamVR_Action_Boolean standUp = SteamVR_Input.GetBooleanAction("StandUp");

        private Vector3 _lastPos;

        [Header("Debug")]
        public bool debugMode = false;
        public Vector2 debugDirection = new Vector3(0, 1);

        private bool isCrouched = false;

        private float speed = 0.0f;

        void Update()
        {
            MovePlayer();

            bool crouchAction = crouch.GetStateDown(SteamVR_Input_Sources.RightHand) || Input.GetKeyDown(KeyCode.L);
            if (crouchAction && !isCrouched)
            {
                Crouch();
            }

            bool standUp = this.standUp.GetStateDown(SteamVR_Input_Sources.RightHand) || Input.GetKeyDown(KeyCode.O);
            if (standUp && isCrouched)
            {
                StandUp();
            }
        }

        private void MovePlayer()
        {
            Vector2 moveValueInput;
            if (debugMode)
            {
                moveValueInput = debugDirection;
            }
            else
            {
                moveValueInput = moveValue.axis;
            }
            
            if (moveValueInput != Vector2.zero)
            {
                Vector3 direction =
                    Player.instance.hmdTransform.TransformDirection(new Vector3(moveValueInput.x, 0,
                        moveValueInput.y)).normalized;

                speed = moveValueInput.magnitude * sensitivity;
                speed = Mathf.Clamp(speed, 0, maxSpeed);

                if (!AnyObstacleInDirection(direction))
                {
                    transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up);
                    footsteps.PlayDelayed(delay);

                }

            


            }
            
            if (TooCloseObstacle())
            {
                Vector3 direction = (_lastPos - playerTransform.position).normalized;
                transform.position += speed * Time.deltaTime * Vector3.ProjectOnPlane(direction, Vector3.up); 
            }
            else
            {
                _lastPos = playerTransform.position;
            }

            
        }

        private bool AnyObstacleInDirection(Vector3 direction)
        {
            RaycastHit hit;

            Vector3 point1 = PositionWihoutCrouch();
            Vector3 point2 = PositionWihoutCrouch();
            point1.y = 0.01f;
            point2.y += height/2;

            point1 -= direction * distance;
            point2 -= direction * distance;

            // CapsuleCollider capsuleCollider = GetComponent<CapsuleCollider>();
            // capsuleCollider.radius = radious;
            // capsuleCollider.height = height;
            // capsuleCollider.center = new Vector3(0, point1.y,0);
            
            
            // Debug.Log($"({point1}; {point2})");
            // Debug.DrawLine(point1, point2);

            int maskNumber = LayerMask.NameToLayer("Obstacles");
            int maskFilter = 1 << maskNumber;
            
            if (Physics.CapsuleCast(point1, point2, radious, direction, out hit, distance, maskFilter))
            {
                // Debug.Log("obstable " + hit.transform.gameObject.name + " layer: " + hit.transform.gameObject.layer);
                return true;
            }

            // Debug.Log("Mover");
            return false;
        }

        private bool TooCloseObstacle()
        {

            Vector3 point2 = PositionWihoutCrouch();
            point2.y -= height/2;
            
            int maskNumber = LayerMask.NameToLayer("Obstacles");
            int maskFilter = 1 << maskNumber;
            
            if (Physics.CheckSphere(point2, radious, maskFilter))
            {
                return true;
            }
            return false;
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

        private Vector3 PositionWihoutCrouch()
        {
            Vector3 ret;
            if (isCrouched)
            {
                ret = playerTransform.position ;
            }
            else
            {
                ret = playerTransform.position + crouchDistance * Vector3.up ;
            }

            return ret;

        }
    }
}