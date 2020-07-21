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
    private float leftScreenBorder;
    [SerializeField]
    private float rightScreenBorder;

    // Start is called before the first frame update
    void Start()
    {
        // Hide cursor
        Cursor.visible = false;

        FindScreenBorders();
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseInput();

        CheckBoundaries();

        if (Input.GetKeyDown(projectileFireButton))
        {
            Instantiate(projectile, transform.position, projectile.transform.rotation);
        }
    }

    // Calculates the location of the screen borders
    void FindScreenBorders()
    {
        // Get width of the player character
        float playerWidth = transform.localScale.x;

        // Get the aproximate width of the screen
        float screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;

        // Find left and right screen borders
        leftScreenBorder = -(screenHalfWidthInWorldUnits - playerWidth);
        rightScreenBorder = screenHalfWidthInWorldUnits - playerWidth;
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
        if (transform.position.x < leftScreenBorder)
        {
            transform.position = new Vector3(leftScreenBorder, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightScreenBorder)
        {
            transform.position = new Vector3(rightScreenBorder, transform.position.y, transform.position.z);
        }
    }
}
