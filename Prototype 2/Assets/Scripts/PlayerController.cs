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

    [Header("Debug")]
    [SerializeField]
    private float viewportPadding = 0.1f;

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
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        float leftViewportBorder = 0f + viewportPadding;
        float rightViewportBorder = 1f - viewportPadding;

        if (viewPos.x < leftViewportBorder)
        {
            transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(leftViewportBorder, viewPos.y, viewPos.z)).x, transform.position.y, transform.position.z);
        }
        if (viewPos.x > rightViewportBorder)
        {
            transform.position = new Vector3(Camera.main.ViewportToWorldPoint(new Vector3(rightViewportBorder, viewPos.y, viewPos.z)).x, transform.position.y, transform.position.z);
        }
    }
}
