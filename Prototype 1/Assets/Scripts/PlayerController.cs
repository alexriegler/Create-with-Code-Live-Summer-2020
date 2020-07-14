using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 20.0f;
    private float turnSpeed = 45.0f;

    public string horizontalAxisName;
    public string verticalAxisName;

    private float horizontalInput;
    private float forwardInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis(horizontalAxisName);
        forwardInput = Input.GetAxis(verticalAxisName);
    }

    void FixedUpdate()
    {
        // Moves the car forward based on vertical input
        transform.Translate(Vector3.forward * speed * forwardInput * Time.fixedDeltaTime);
        // Rotates the car based on horizontal input
        transform.Rotate(Vector3.up, turnSpeed * horizontalInput * Time.fixedDeltaTime);
    }
}
