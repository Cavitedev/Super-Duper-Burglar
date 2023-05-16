using Project.Scripts;
using UnityEngine;
using Valve.VR.InteractionSystem;
using UnityEngine.AI;
using UnityEngine.Serialization;
using System;
using System.Collections;

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
    [SerializeField] private float loseRadius = 1f;
    public float detectionAngle = 90.0f;

    public bool movementLoop;
    private bool _inRange = false, _reverse_path = false;

    [Header("Animation")] public Animator animator;
    private int _maskRayFilter;
    float _timeUntilLost;
    private int currentPoint;
    private Transform playerTransform;



    public AudioSource footsteps;
    public AudioSource creepySounds;
    public AudioClip scream;
    private bool soundPlayed = false;
    private bool isStopped = false;
    public float delay = 1f;
    private float _lastSoundTime;

    private void Start()
    {
        _maskRayFilter = 1 << Consts.ObstaclesLayer | 1 << Consts.PlayerLayer;
        //whisperSounds.Play();
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
        if ((!_inRange || playerTransform == null))
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
                        PlayFootstep();
                    }
                    else
                    {
                        currentPoint = currentPoint - 1;
                        agent.SetDestination(pathPoints[currentPoint].position);
                        PlayFootstep();
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
        else if(!isStopped)
        {
            agent.SetDestination(playerTransform.position);
            PlayFootstep();
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


        if (toPlayer.magnitude <= detectionRadius && !isStopped)
        {
            if (Vector3.Dot(toPlayer.normalized, transform.forward) >
                Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad))
            {
                if (RaycastPlayer())
                {
                    if (!soundPlayed)
                        PlayGritoCuandoEncuentra();

                    if (toPlayer.magnitude <= loseRadius)
                    {
                        StartCoroutine(enemyTePilla());
                        //PlayerStats.Instance.GameOver(false);
                    }
                    
                    _timeUntilLost = timeUntilLost;
                    _inRange = true;
                    agent.SetDestination(Player.instance.transform.position);
                    return Player.instance.transform;
                    

                    
                }
            }
        }

        if (_inRange && !isStopped)
        {
            _timeUntilLost -= Time.deltaTime;
            agent.SetDestination(Player.instance.transform.position);

        }
        if (_timeUntilLost <= 0f && _inRange) //Aun no ha entrado a el if
        {
            agent.SetDestination(pathPoints[currentPoint].position);
            _inRange = false;
            soundPlayed = false;
            return null;
        }


        return null;
    }

    IEnumerator enemyTePilla()
    {
        Debug.Log("te pille");
        _timeUntilLost = 0;
        isStopped = true;
        agent.SetDestination(pathPoints[currentPoint].position);
        yield return new WaitForSeconds(5f);
        isStopped = false;
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

    private void PlayFootstep()
    {
        float currentTime = Time.time;
        if (_lastSoundTime + delay < currentTime)
        {
            _lastSoundTime = Time.time;
            footsteps.Play();

        }
    }

    private void PlayGritoCuandoEncuentra()
    {
        soundPlayed = true;
        creepySounds.PlayOneShot(scream);
    }
}