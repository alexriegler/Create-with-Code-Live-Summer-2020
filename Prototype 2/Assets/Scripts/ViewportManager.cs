using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The ViewportManager class.
 * Contains methods to manage and access viewport information.
 */
/// <summary>
/// The <c>ViewportManager</c> class.
/// Contains methods to manage and access viewport information.
/// </summary>
public class ViewportManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField]
    private Camera gameCamera;

    [Header("Viewport")]
    [SerializeField]
    private float vpSidePadding = 0.1f;
    private float vpLeftBorderX = 0f;
    private float vpRightBorderX = 1f;
    private float vpBottomBorderY = 0f;
    private float vpTopBorderY = 1f;

    private Vector3 vpOrigin;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = Camera.main;
        vpLeftBorderX = vpLeftBorderX + vpSidePadding;
        vpRightBorderX = vpRightBorderX - vpSidePadding;

        vpOrigin = gameCamera.WorldToViewportPoint(Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Properties

    // Gets the amount of padding within the viewport.
    /// <summary>
    /// Gets the amount of padding within the viewport.
    /// </summary>
    public float VpSidePadding { get => vpSidePadding; }

    // Gets the x component of the left border position in viewport space.
    /// <summary>
    /// Gets the x component of the left border position in viewport space.
    /// </summary>
    public float VpLeftBorderX { get => vpLeftBorderX; }

    // Gets the x component of the left border position in world space.
    /// <summary>
    /// Gets the x component of the left border position in world space.
    /// </summary>
    public float WLeftBorderX
    {
        get
        {
            Vector3 vpLeftBorderPos = new Vector3(VpLeftBorderX, VpOriginY, VpOriginZ);
            return gameCamera.ViewportToWorldPoint(vpLeftBorderPos).x;
        }
    }

    // Gets the x component of the right border position in viewport space.
    /// <summary>
    /// Gets the x component of the right border position in viewport space.
    /// </summary>
    public float VpRightBorderX { get => vpRightBorderX; }

    // Gets the x component of the right border position in world space.
    /// <summary>
    /// Gets the x component of the right border position in world space.
    /// </summary>
    public float WRightBorderX
    {
        get
        {
            Vector3 vpRightBorderPos = new Vector3(VpRightBorderX, VpOriginY, VpOriginZ);
            return gameCamera.ViewportToWorldPoint(vpRightBorderPos).x;
        }
    }

    // Gets the coordinates of the world origin in viewport space coordinates.
    /// <summary>
    /// Gets the coordinates of the world origin in viewport space coordinates.
    /// </summary>
    public Vector3 VpOrigin { get => vpOrigin; }

    // Gets the x component of the viewport space coordinates of the world origin.
    /// <summary>
    /// Gets the x component of the viewport space coordinates of the world origin.
    /// </summary>
    public float VpOriginX { get => vpOrigin.x; }

    // Gets the y component of the viewport space coordinates of the world origin.
    /// <summary>
    /// Gets the y component of the viewport space coordinates of the world origin.
    /// </summary>
    public float VpOriginY { get => vpOrigin.y; }

    // Gets the z component of the viewport space coordinates of the world origin.
    /// <summary>
    /// Gets the z component of the viewport space coordinates of the world origin.
    /// </summary>
    public float VpOriginZ { get => vpOrigin.z; }
}
