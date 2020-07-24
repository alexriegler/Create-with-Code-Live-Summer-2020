using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    public Camera gameCamera;
    private ViewportManager vpManager;

    [Header("Player")]
    public GameObject projectile;

    [SerializeField]
    private float fireRate = 0.25f;
    private float nextShotTime;

    [Header("Player")]
    [SerializeField]
    private KeyCode projectileFireButton = KeyCode.Space;

    // Start is called before the first frame update
    void Start()
    {
        // Get viewport manager
        vpManager = gameCamera.GetComponent<ViewportManager>();
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseInput();

        CheckBoundaries();

        // Fire a projectile
        if ((Input.GetKey(projectileFireButton) && (Time.time > nextShotTime)) || Input.GetKeyDown(projectileFireButton))
        {
            nextShotTime = Time.time + fireRate;
            Instantiate(projectile, transform.position, projectile.transform.rotation);
        }
    }

    // Uses a ray to point at the location on the screen where the player should move to
    void GetMouseInput()
    {
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            transform.position = new Vector3(hitInfo.point.x, transform.position.y, hitInfo.point.z);
        }
    }

    // Restricts the player's movement within the boundaries
    void CheckBoundaries()
    {
        Vector3 vpPos = gameCamera.WorldToViewportPoint(transform.position);

        // Check left and right borders
        if (vpPos.x < vpManager.VpLeftBorderX)
        {
            float xPos = vpManager.WLeftBorderX;
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }
        else if (vpPos.x > vpManager.VpRightBorderX)
        {
            float xPos = vpManager.WRightBorderX;
            transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
        }
        
        // Check bottom and top borders
        if (vpPos.y < vpManager.VpBottomBorderY)
        {
            float zPos = vpManager.WBottomBorderZ;
            transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
        }
        else if (vpPos.y > vpManager.VpTopBorderY)
        {
            float zPos = vpManager.WTopBorderZ;
            transform.position = new Vector3(transform.position.x, transform.position.y, zPos);
        }
    }
}
