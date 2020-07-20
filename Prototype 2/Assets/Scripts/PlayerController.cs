using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private float speed = 10.0f;

    [Header("Input")]
    [SerializeField]
    private string horizontalAxisName = "Horizontal";
    [SerializeField]
    private float horizontalInput;

    [SerializeField]
    private Camera gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseInput();

        // Check boundaries
        if (transform.position.x < -10f)
        {
            transform.position = new Vector3(-10f, transform.position.y, transform.position.z);
        }

        // Move player
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
    }

    void GetMouseInput()
    {
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            // If mouse pointer is to the left of player, move left
            if (transform.position.x > hitInfo.point.x)
            {
                horizontalInput = -1f;
            }
            else if (transform.position.x < hitInfo.point.x)
            {
                horizontalInput = 1f;
            }
            else
            {
                horizontalInput = 0;
            }
        }
    }
}
