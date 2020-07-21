using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera gameCamera;
    [SerializeField]
    private float screenHalfWidthInWorldUnits;

    // Start is called before the first frame update
    void Start()
    {
        float halfPlayerWidth = transform.localScale.x;
        // Get the aproximate width of the screen
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize - halfPlayerWidth;
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseInput();

        // Check boundaries
        if (transform.position.x < -screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector3(-screenHalfWidthInWorldUnits, transform.position.y, transform.position.z);
        }
        if (transform.position.x > screenHalfWidthInWorldUnits)
        {
            transform.position = new Vector3(screenHalfWidthInWorldUnits, transform.position.y, transform.position.z);
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
