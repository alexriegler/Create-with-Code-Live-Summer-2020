﻿using UnityEngine;

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
    private float vpPadding = 0.1f;
    private Vector3 vpOrigin;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = Camera.main;
        VpLeftBorderX = VpLeftBorderX + vpPadding;
        VpRightBorderX = VpRightBorderX - vpPadding;
        VpBottomBorderY = VpBottomBorderY + vpPadding;
        VpTopBorderY = VpTopBorderY - vpPadding;

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
    public float VpSidePadding { get => vpPadding; }

    // The x component of the left border position in viewport space.
    /// <summary>
    /// The x component of the left border position in viewport space.
    /// </summary>
    public float VpLeftBorderX { get; private set; } = 0f;

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
    public float VpRightBorderX { get; private set; } = 1f;

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
    public float VpBottomBorderY { get; private set; } = 0f;

    // The z component of the bottom border position in world space.
    /// <summary>
    /// The z component of the bottom border position in world space.
    /// </summary>
    public float WBottomBorderZ
    {
        get
        {
            Vector3 vpBottomBorderPos = new Vector3(VpOriginX, VpBottomBorderY, VpOriginZ);
            return gameCamera.ViewportToWorldPoint(vpBottomBorderPos).z;
        }
    }

    // The y component of the top border position in viewport space.
    /// <summary>
    /// The y component of the top border position in viewport space.
    /// </summary>
    public float VpTopBorderY { get; private set; } = 1f;

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
