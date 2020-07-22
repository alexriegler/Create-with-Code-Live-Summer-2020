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
    private float vpSidePadding = 0.1f;
    [SerializeField]
    private float vpLeftBorderX = 0f;
    [SerializeField]
    private float vpRightBorderX = 1f;
    [SerializeField]
    private float vpBottomBorderY = 0f;
    [SerializeField]
    private float vpTopBorderY = 1f;

    private Vector3 vpOrigin;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = Camera.main;
        vpLeftBorderX = 0f + vpSidePadding;
        vpRightBorderX = 1f - vpSidePadding;

        vpOrigin = gameCamera.WorldToViewportPoint(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Properties

    // Get the amount of padding within the viewport
    public float VpSidePadding
    {
        get
        {
            return vpSidePadding;
        }
    }

    // Get the x component of the left border position in viewport space
    public float VpLeftBorderX
    {
        get
        {
            return vpLeftBorderX;
        }
    }

    // Get the x component of the left border position in world space
    public float WLeftBorderX
    {
        get
        {
            Vector3 vpLeftBorderPos = new Vector3(VpLeftBorderX, VpOriginY, VpOriginZ);
            return gameCamera.ViewportToWorldPoint(vpLeftBorderPos).x;
        }
    }

    // Get the x component of the right border position in viewport space
    public float VpRightBorderX
    {
        get
        {
            return vpRightBorderX;
        }
    }

    // Get the x component of the right border position in world space
    public float WRightBorderX
    {
        get
        {
            Vector3 vpRightBorderPos = new Vector3(VpRightBorderX, VpOriginY, VpOriginZ);
            return gameCamera.ViewportToWorldPoint(vpRightBorderPos).x;
        }
    }

    // Get the coordinates of the world origin in viewport space coordinates
    public Vector3 VpOrigin
    {
        get
        {
            return vpOrigin;
        }
    }

    // Get the x component of the viewport space coordinates of the world origin
    public float VpOriginX
    {
        get
        {
            return vpOrigin.x;
        }
    }

    // Get the y component of the viewport space coordinates of the world origin
    public float VpOriginY
    {
        get
        {
            return vpOrigin.y;
        }
    }

    // Get the z component of the viewport space coordinates of the world origin
    public float VpOriginZ
    {
        get
        {
            return vpOrigin.z;
        }
    }
}
