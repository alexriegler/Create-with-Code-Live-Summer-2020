using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera gameCamera;

    [SerializeField]
    private float leftScreenBorder;
    [SerializeField]
    private float rightScreenBorder;

    // Start is called before the first frame update
    void Start()
    {
        // Get width of the player character
        float playerWidth = transform.localScale.x;

        // Get the aproximate width of the screen
        float screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;

        // Find left and right screen borders
        leftScreenBorder = -(screenHalfWidthInWorldUnits - playerWidth);
        rightScreenBorder = screenHalfWidthInWorldUnits - playerWidth;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseInput();

        // Check boundaries
        if (transform.position.x < leftScreenBorder)
        {
            transform.position = new Vector3(leftScreenBorder, transform.position.y, transform.position.z);
        }
        if (transform.position.x > rightScreenBorder)
        {
            transform.position = new Vector3(rightScreenBorder, transform.position.y, transform.position.z);
        }
    }

    void GetMouseInput()
    {
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        if (Physics.Raycast(ray, out hitInfo))
        {
            transform.position = new Vector3(hitInfo.point.x, transform.position.y, transform.position.z);
        }
    }
}
