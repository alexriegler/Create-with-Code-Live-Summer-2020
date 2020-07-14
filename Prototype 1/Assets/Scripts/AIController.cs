using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    private float speed = 15.0f;
    // private float turnSpeed = 45.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Moves the vehicle forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        // Rotates the car based on horizontal input
        // transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.deltaTime);
    }
}
