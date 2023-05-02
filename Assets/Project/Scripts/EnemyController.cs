using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    //AI
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Camera _camera;

    //Path 

    [SerializeField] Transform[] pathPoints;
    private int currentPoint;
    //Detection
    [SerializeField] float enemyRotationSpeed = 100f;
    [SerializeField] float enemySpeed = 5f;
    [SerializeField] float timeUntilLost = 5f;
    float _timeUntilLost;

    public float detectionRadius = 10.0f;
    public float detectionAngle = 90.0f;

    public bool inRange = false, reverse_path = false;
    private Transform playerTransform;

    [Header("Animation")] public Animator animator;

    private void Start()
    {
        
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
        
        
        animator.SetFloat("Forward",agent.velocity.magnitude / agent.speed);
        animator.SetFloat("Turn",angle / 90);
    }


    private void EnemyMovement()
    {
        if (!inRange || playerTransform == null) //TODO Cambiar esto por un sistema de puntos en plan patrulla
        {
            /*if (Input.GetMouseButtonDown(1))
            {

                Ray movePosition = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(movePosition, out var hitInfo))
                {
                    
                    agent.SetDestination(hitInfo.point);
                }
            }*/

            Vector3 enemyPosition = transform.position;
            Vector3 toPoint = pathPoints[currentPoint].position - enemyPosition;
            toPoint.y = 0;
            
            if (toPoint.magnitude <= detectionRadius / 2)
            {
                if (Vector3.Dot(toPoint.normalized, transform.forward) >
                    Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
                {
                    if(reverse_path == false)
                    {
                        currentPoint = currentPoint + 1;
                        agent.SetDestination(pathPoints[currentPoint].position);
                    }
                    else
                    {
                        currentPoint = currentPoint - 1;
                        agent.SetDestination(pathPoints[currentPoint].position);
                    }

                    if(currentPoint == pathPoints.Length - 1)
                    {
                        reverse_path = true;
                    }
                    if(currentPoint == 0)
                    {
                        reverse_path = false;
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
        if (PlayerController.Instance == null)
        {
           
            return null;
        }

        Vector3 enemyPosition = transform.position;
        Vector3 toPlayer = PlayerController.Instance.transform.position - enemyPosition;
        toPlayer.y = 0;

        if (toPlayer.magnitude <= detectionRadius)
        {
            if (Vector3.Dot(toPlayer.normalized, transform.forward) >
                Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad)) {
                _timeUntilLost = timeUntilLost;
                inRange = true;
                Debug.Log("Player has been detected!");
                return PlayerController.Instance.transform; 
            }
        }
        else // Funcion de busqueda del jugador cuando se pierde su rastro
        {

            if (_timeUntilLost <= 0f && inRange) //Aun no ha entrado a el if
            {
                _timeUntilLost = 0f;
                inRange = false;
                return null;
            }
            else if(_timeUntilLost <= 0f && !inRange) //Ya acabó el tiempo y se devolvió la posicion
            {
                _timeUntilLost = 0f;
                inRange = false;
                agent.SetDestination(pathPoints[currentPoint].position);
                return null;
            }
            else //Aun sabe donde esta el jugador
            {
                _timeUntilLost -= Time.deltaTime; //Tiempo va bajando mientras esté fuera de rango
                inRange = true;
                return PlayerController.Instance.transform;
            }
           
        }


        return null;
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
}
