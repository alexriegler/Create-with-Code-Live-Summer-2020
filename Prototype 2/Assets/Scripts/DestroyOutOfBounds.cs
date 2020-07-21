using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Should the object be destroyed when crossing the left border of the viewport.")]
    private bool leftBorder = true;
    [SerializeField]
    [Tooltip("Should the object be destroyed when crossing the right border of the viewport.")]
    private bool rightBorder = true;
    [SerializeField]
    [Tooltip("Should the object be destroyed when crossing the top border of the viewport.")]
    private bool topBorder = true;
    [SerializeField]
    [Tooltip("Should the object be destroyed when crossing the bottom border of the viewport.")]
    private bool bottomBorder = true;

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
        if ((leftBorder && viewPos.x < 0f) 
            || (rightBorder && viewPos.x > 1f) 
            || (topBorder && viewPos.y > 1f) 
            || (bottomBorder && viewPos.y < 0f))
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}
