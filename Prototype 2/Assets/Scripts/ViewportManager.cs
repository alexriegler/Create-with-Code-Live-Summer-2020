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

    // The amount of padding within the viewport.
    /// <summary>
    /// The amount of padding within the viewport.
    /// </summary>
    public float VpSidePadding { get => vpSidePadding; }

    // The x component of the left border position in viewport space.
    /// <summary>
    /// The x component of the left border position in viewport space.
    /// </summary>
    public float VpLeftBorderX { get => vpLeftBorderX; }

    // The x component of the left border position in world space.
    /// <summary>
    /// The x component of the left border position in world space.
    /// </summary>
    public float WLeftBorderX
    {
        get
        {
            Vector3 vpLeftBorderPos = new Vector3(VpLeftBorderX, VpOriginY, VpOriginZ);
            return gameCamera.ViewportToWorldPoint(vpLeftBorderPos).x;
        }
    }

    // The x component of the right border position in viewport space.
    /// <summary>
    /// The x component of the right border position in viewport space.
    /// </summary>
    public float VpRightBorderX { get => vpRightBorderX; }

    // The x component of the right border position in world space.
    /// <summary>
    /// The x component of the right border position in world space.
    /// </summary>
    public float WRightBorderX
    {
        get
        {
            Vector3 vpRightBorderPos = new Vector3(VpRightBorderX, VpOriginY, VpOriginZ);
            return gameCamera.ViewportToWorldPoint(vpRightBorderPos).x;
        }
    }

    // The y component of the bottom border position in viewport space.
    /// <summary>
    /// The y component of the bottom border position in viewport space.
    /// </summary>
    public float VpBottomBorderY { get => vpBottomBorderY; }

    // The y component of the bottom border position in world space.
    /// <summary>
    /// The y component of the bottom border position in world space.
    /// </summary>
    public float WBottomBorderY
    {
        get
        {
            Vector3 vpBottomBorderPos = new Vector3(VpOriginX, VpBottomBorderY, VpOriginZ);
            return gameCamera.ViewportToWorldPoint(vpBottomBorderPos).y;
        }
    }

    // The y component of the top border position in viewport space.
    /// <summary>
    /// The y component of the top border position in viewport space.
    /// </summary>
    public float VpTopBorderY { get => vpTopBorderY; }

    // The z component of the top border position in world space.
    /// <summary>
    /// The z component of the top border position in world space.
    /// </summary>
    public float WTopBorderZ
    {
        get
        {
            Vector3 vpTopBorderPos = new Vector3(VpOriginX, VpTopBorderY, VpOriginZ);
            return gameCamera.ViewportToWorldPoint(vpTopBorderPos).z;
        }
    }

    // The coordinates of the world origin in viewport space coordinates.
    /// <summary>
    /// The coordinates of the world origin in viewport space coordinates.
    /// </summary>
    public Vector3 VpOrigin { get => vpOrigin; }

    // The x component of the viewport space coordinates of the world origin.
    /// <summary>
    /// The x component of the viewport space coordinates of the world origin.
    /// </summary>
    public float VpOriginX { get => vpOrigin.x; }

    // The y component of the viewport space coordinates of the world origin.
    /// <summary>
    /// The y component of the viewport space coordinates of the world origin.
    /// </summary>
    public float VpOriginY { get => vpOrigin.y; }

    // The z component of the viewport space coordinates of the world origin.
    /// <summary>
    /// The z component of the viewport space coordinates of the world origin.
    /// </summary>
    public float VpOriginZ { get => vpOrigin.z; }
}
