using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private float speed = 30.0f;
    private float rotationSpeed = 120.0f;
    public float verticalInput;

    public bool invertControls = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // get the user's vertical input
        verticalInput = Input.GetAxis("Vertical");

        // move the plane forward at a constant rate
        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        // tilt the plane up/down based on up/down arrow keys
        if (invertControls)
        {
            transform.Rotate(Vector3.right, rotationSpeed * verticalInput * Time.deltaTime);
        }
        else
        {
            transform.Rotate(Vector3.right, -rotationSpeed * verticalInput * Time.deltaTime);
        }
        
    }
}
