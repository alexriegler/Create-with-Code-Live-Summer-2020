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
    private string horizontalAxisName;
    [SerializeField]
    private float horizontalInput;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis(horizontalAxisName);

        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
    }
}
