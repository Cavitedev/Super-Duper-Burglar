using Project.Scripts;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    //AI
    [SerializeField] NavMeshAgent agent;

    //Path 

    [SerializeField] Transform[] pathPoints;
    //Detection
    [SerializeField] private Transform[] eyes;
    [SerializeField] float enemyRotationSpeed = 100f;

    [SerializeField] float timeUntilLost = 5f;

    public float detectionRadius = 10.0f;
    public float detectionAngle = 90.0f;

    public bool movementLoop;
    private bool _inRange = false, _reverse_path = false;

    [Header("Animation")] public Animator animator;
    private int _maskRayFilter;
    float _timeUntilLost;
    private int currentPoint;
    private Transform playerTransform;

    private void Start()
    {
        _maskRayFilter = 1 << Consts.ObstaclesLayer | 1 << Consts.PlayerLayer;

        currentPoint = 0;
        _timeUntilLost = 0f;
        agent.angularSpeed = enemyRotationSpeed;

        //El enemigo va al primer punto
        agent.SetDestination(pathPoints[currentPoint].position);
    }

    private void Update()
    {
        playerTransform = LookForPlayer();
        EnemyMovement();

        float angle = Vector3.SignedAngle(transform.forward, agent.velocity, Vector3.up);
        // Debug.Log("Forward: " + agent.velocity.magnitude / agent.speed);
        // Debug.Log("Turn Angle: " + angle);


        animator.SetFloat("Forward", agent.velocity.magnitude / agent.speed);
        animator.SetFloat("Turn", angle / 90);
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Color c = new Color(0.8f, 0, 0, 0.4f);
        UnityEditor.Handles.color = c;

        Vector3 rotatedForward = Quaternion.Euler(
            0,
            -detectionAngle * 0.5f,
            0) * transform.forward;

        UnityEditor.Handles.DrawSolidArc(
            transform.position,
            Vector3.up,
            rotatedForward,
            detectionAngle,
            detectionRadius);
    }
#endif


    private void EnemyMovement()
    {
        if (!_inRange || playerTransform == null)
        {
            Vector3 enemyPosition = transform.position;
            Vector3 toPoint = pathPoints[currentPoint].position - enemyPosition;
            toPoint.y = 0;

            if (toPoint.magnitude <= detectionRadius / 2)
            {
                if (Vector3.Dot(toPoint.normalized, transform.forward) >
                    Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
                {
                    if (!_reverse_path || movementLoop)
                    {
                        currentPoint = currentPoint + 1;

                        if (currentPoint == pathPoints.Length && movementLoop)
                        {
                            currentPoint = 0;
                        }

                        agent.SetDestination(pathPoints[currentPoint].position);
                    }
                    else
                    {
                        currentPoint = currentPoint - 1;
                        agent.SetDestination(pathPoints[currentPoint].position);
                    }

                    if (!movementLoop)
                    {
                        if (currentPoint == pathPoints.Length - 1)
                        {
                            _reverse_path = true;
                        }

                        if (currentPoint == 0)
                        {
                            _reverse_path = false;
                        }
                    }


                    Debug.Log("Point has been detected!");
                }
            }
        }
        else
        {
            agent.SetDestination(playerTransform.position);
        }
    }
    //TODO CAMBIAR PLAYERCONTROLLER.Instance POR Player.instance

    private Transform LookForPlayer() //private Player LookForPlayer()
    {
        if (Player.instance == null)
        {
            return null;
        }

        Vector3 enemyPosition = transform.position;
        Vector3 toPlayer = Player.instance.transform.position - enemyPosition;
        toPlayer.y = 0;


        if (toPlayer.magnitude <= detectionRadius)
        {
            if (Vector3.Dot(toPlayer.normalized, transform.forward) >
                Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
            {
                if (RaycastPlayer())
                {
                    _timeUntilLost = timeUntilLost;
                    _inRange = true;
                    Debug.Log("Player has been detected!");
                    return Player.instance.transform;
                }
            }
        }

        if (_inRange)
        {
            _timeUntilLost -= Time.deltaTime;
        }
        if (_timeUntilLost <= 0f && _inRange) //Aun no ha entrado a el if
        {
            agent.SetDestination(pathPoints[currentPoint].position);
            _inRange = false;
            return null;
        }


        return null;
    }

    private bool RaycastPlayer()
    {
        foreach (Transform eye in eyes)
        {
            RaycastHit hit;
            Vector3 toPlayer = Player.instance.hmdTransform.position - eye.position;

            Debug.DrawRay(eye.position, toPlayer, Color.green);
            if (Physics.Raycast(eye.position, toPlayer, out hit, detectionRadius, _maskRayFilter))
            {
                if (hit.transform.gameObject.layer == Consts.PlayerLayer)
                {
                    return true;
                }
            }
        }

        return false;
    }
}