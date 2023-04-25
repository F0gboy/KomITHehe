using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotationTest : MonoBehaviour
{
    public GameObject sphere;
    public float speed = 5f;

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 inputVector = new Vector3(horizontalInput, 0f, verticalInput).normalized;

        transform.Translate(inputVector * speed * Time.deltaTime);

        float distanceFromCenter = Vector3.Distance(transform.position, sphere.transform.position);
        if (distanceFromCenter < sphere.transform.localScale.x / 2f)
        {
            transform.position = transform.position.normalized * (sphere.transform.localScale.x / 2f);
        }

        transform.LookAt(sphere.transform.position);
    }
}
