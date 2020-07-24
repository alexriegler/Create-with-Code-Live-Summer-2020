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
    public Camera gameCamera;

    [Header("Viewport")]
    [SerializeField]
    private float vpPadding = 0.1f;
    private Vector3 vpOrigin;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = Camera.main;

        // Add padding to viewport border coordinates
        VpLeftBorderX += vpPadding;
        VpRightBorderX -= vpPadding;
        VpBottomBorderY += vpPadding;
        VpTopBorderY -= vpPadding;

        // Get the origin position in viewport coordinates
        vpOrigin = gameCamera.WorldToViewportPoint(Vector3.zero);
    }

    // Properties
    #region Viewport Border properties
    #region Viewport Left Border
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
    #endregion

    #region Viewport Right Border
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
    #endregion

    #region Viewport Bottom Border
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
    #endregion

    #region Viewport Top Border
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
    #endregion
    #endregion

    #region Viewport Origin properties
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
    #endregion
}
