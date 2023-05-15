using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class MoveAroundPlayer : MonoBehaviour
{
    private Transform _playerCamera;
    public float distancePlayer = 2f;
    public float updateSpeed = 5f;

    // Start is called before the first frame update
    void Start()
    {
        _playerCamera = Player.instance.hmdTransform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - _playerCamera.position, Vector3.up);

        float angle = _playerCamera.rotation.eulerAngles.y - 90;

        float zDist = Mathf.Sin(angle * Mathf.Deg2Rad) * distancePlayer;
        float xDist = Mathf.Cos(angle * Mathf.Deg2Rad) * distancePlayer;

        transform.position = Vector3.Slerp(transform.position, new Vector3(_playerCamera.position.x + xDist,
            _playerCamera.position.y,
            _playerCamera.position.z - zDist), Time.deltaTime * updateSpeed);
    }
}