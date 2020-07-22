using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField]
    private Camera gameCamera;

    [Header("Player")]
    [SerializeField]
    private GameObject projectile;

    [Header("Player")]
    [SerializeField]
    private KeyCode projectileFireButton = KeyCode.Space;

    [SerializeField]
    private ViewportManager vpManager;

    // Start is called before the first frame update
    void Start()
    {
        // Hide cursor
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseInput();

        CheckBoundaries();

        // Fire a projectile
        if (Input.GetKeyDown(projectileFireButton))
        {
            Instantiate(projectile, transform.position, projectile.transform.rotation);
        }
    }

    // Uses a ray to point at the location on the screen where the player should move to
    void GetMouseInput()
    {
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            transform.position = new Vector3(hitInfo.point.x, transform.position.y, transform.position.z);
        }
    }

    // Restricts the player's movement within the boundaries
    void CheckBoundaries()
    {
        Vector3 vpPos = gameCamera.WorldToViewportPoint(transform.position);

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
    }
}
