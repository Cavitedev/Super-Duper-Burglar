using UnityEngine;
using Valve.VR.InteractionSystem;


public class EnemyController : MonoBehaviour
{
    public float detectionRadius = 10.0f;
    public float detectionAngle = 90.0f;

    private void Update()
    {
        LookForPlayer();
    }

    private Player LookForPlayer()
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
                Mathf.Cos(detectionAngle * 0.5f * Mathf.Deg2Rad)) {

                Debug.Log("Player has been detected!");
                return Player.instance;
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
