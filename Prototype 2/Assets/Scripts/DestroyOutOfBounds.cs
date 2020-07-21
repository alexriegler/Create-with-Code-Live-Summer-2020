using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The amount of time in seconds to pass after exiting the viewport and before destroying.")]
    private float destroyDelay = 0.5f;
    [SerializeField]
    [Tooltip("Positional coordinates of the object in viewport space. The bottom-left of the camera is (0,0); the top-right is (1,1).")]
    private Vector3 viewPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        viewPos = Camera.main.WorldToViewportPoint(transform.position);

        // If object leaves viewport space, destroy
        if (viewPos.x < 0f || viewPos.x > 1f || viewPos.y > 1f || viewPos.y < 0f)
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
