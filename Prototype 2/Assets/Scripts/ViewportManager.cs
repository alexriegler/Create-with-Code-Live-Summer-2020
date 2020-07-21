using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewportManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField]
    private Camera gameCamera;

    [Header("Viewport")]
    [SerializeField]
    private float viewportPadding = 0.1f;
    [SerializeField]
    private float leftViewportBorder;
    [SerializeField]
    private float rightViewportBorder;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = Camera.main;
        leftViewportBorder = 0f + viewportPadding;
        rightViewportBorder = 1f - viewportPadding;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Get the amount of padding inside the viewport
    public float ViewportPadding
    {
        get
        {
            return viewportPadding;
        }
    }

    // Get the x value of the left border location in viewport space
    public float LeftViewportBorder
    {
        get
        {
            return leftViewportBorder;
        }
    }

    // Get the x value of the right border location in viewport space
    public float RightViewportBorder
    {
        get
        {
            return rightViewportBorder;
        }
    }
}
