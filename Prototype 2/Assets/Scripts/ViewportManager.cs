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
    private float vpPadding = 0.1f;
    [SerializeField]
    private float vpLeftBorderX;
    [SerializeField]
    private float vpRightBorderX;

    private Vector3 vpOrigin;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = Camera.main;
        vpLeftBorderX = 0f + vpPadding;
        vpRightBorderX = 1f - vpPadding;

        vpOrigin = gameCamera.WorldToViewportPoint(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Properties

    // Get the amount of padding inside the viewport
    public float VpPadding
    {
        get
        {
            return vpPadding;
        }
    }

    // Get the x value of the left border location in viewport space
    public float VpLeftBorderX
    {
        get
        {
            return vpLeftBorderX;
        }
    }

    // Get the x value of the right border location in viewport space
    public float VpRightBorderX
    {
        get
        {
            return vpRightBorderX;
        }
    }

    // Get the coordinates of the world origin in viewport coordinates
    public Vector3 VpOrigin
    {
        get
        {
            return vpOrigin;
        }
    }

    // Get the x component of the viewport coordinates of the world origin
    public float VpOriginX
    {
        get
        {
            return vpOrigin.x;
        }
    }

    // Get the y component of the viewport coordinates of the world origin
    public float VpOriginY
    {
        get
        {
            return vpOrigin.y;
        }
    }

    // Get the z component of the viewport coordinates of the world origin
    public float VpOriginZ
    {
        get
        {
            return vpOrigin.z;
        }
    }
}
