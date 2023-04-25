using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform target; // the point around which the camera rotates
    public float speed = 1f; // rotation speed in degrees per second
    public float moveSpeed = 5f; // move speed in units per second
    public float minDistance = 1f; // minimum distance between camera and target

    public bool isMoving;

    private Vector3 axis; // the axis of rotation
    private float initialHeight = 10; // the initial height of the camera relative to the target

    private float zoom = 8f;

    void Start()
    {
        // calculate the axis of rotation
        axis = new Vector3(1f, 1f, 0f).normalized;

        // calculate the initial height of the camera relative to the target
        initialHeight = transform.position.y - target.position.y;

        // move the camera closer to the target initially
        MoveTowardsTarget();
    }

    void Update()
    {
        if (!isMoving) return;
        
        /*
        float scrollWheelInput = Input.GetAxis("Mouse ScrollWheel");
        if (scrollWheelInput != 0f)
        {
            zoom -= scrollWheelInput;
            zoom = Mathf.Clamp(zoom, 1f, 10f);
            
            MoveToNextTarget();
        }
        */
        
        // calculate the new position of the camera
        Quaternion rotation = Quaternion.AngleAxis(speed * Time.deltaTime, axis);
        Vector3 offset = transform.position - target.position;
        offset = rotation * offset;
        offset.y = initialHeight; // keep the height constant
        transform.position = target.position + offset;

        // rotate the camera to look at the target
        transform.LookAt(target);

        // check if the camera is too close to the target
        if (Vector3.Distance(transform.position, target.position) < minDistance)
        {
            MoveAwayFromTarget();
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 offset = transform.position - target.position;
        offset = offset.normalized * minDistance;
        transform.position = target.position + offset;
    }

    void MoveAwayFromTarget()
    {
        Vector3 offset = transform.position - target.position;
        offset = offset.normalized * (minDistance + zoom);
        transform.position = target.position + offset;
    }

    public void MoveToNextTarget()
    {
        MoveTowardsTarget();
        MoveAwayFromTarget();
    }
}
