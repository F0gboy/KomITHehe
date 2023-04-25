using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Transform target; // the point around which the camera rotates
    public float speed = 15f; // rotation speed in degrees per second
    public float moveSpeed = 5f; // move speed in units per second
    public float minDistance = 1f; // minimum distance between camera and target
    public float offsetUp = 0.5f; // offset to move the camera up after moving away from the target

    private Vector3 axis; // the axis of rotation
    private float initialHeight; // the initial height of the camera relative to the target
    private bool movingAway = false; // flag to indicate whether the camera is moving away from the target
    private float distanceBeforeMove; // the distance between the camera and the target before moving away

    public bool isMoving = false;

    void Start()
    {
        // calculate the axis of rotation
        axis = new Vector3(1f, 1f, 0f).normalized;

        // calculate the initial height of the camera relative to the target
        initialHeight = transform.position.y - target.position.y;

        // move the camera closer to the target initially
        MoveTowardsTarget();
        MoveAwayFromTarget();
    }

    void Update()
    {
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
        else if (movingAway && Vector3.Distance(transform.position, target.position) > distanceBeforeMove + 0.1f)
        {
            MoveUp();
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
        movingAway = true;
        distanceBeforeMove = Vector3.Distance(transform.position, target.position);

        Vector3 offset = transform.position - target.position;
        offset = offset.normalized * (minDistance + 0.1f);
        transform.position = target.position + offset;
    }

    public void MoveToNextTarget()
    {
        MoveTowardsTarget();
        MoveAwayFromTarget();
    }
    
    void MoveUp()
    {
        Vector3 offset = transform.position - target.position;
        offset.y += offsetUp;
        transform.position = target.position + offset;

        movingAway = false;
    }
}