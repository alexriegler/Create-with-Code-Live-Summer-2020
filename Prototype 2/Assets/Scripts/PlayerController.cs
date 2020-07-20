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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Use mouse for movement
        horizontalInput = Input.GetAxisRaw(horizontalAxisName);

        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
    }
}
