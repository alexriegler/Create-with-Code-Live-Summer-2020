using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseInput();

        // Check boundaries
        if (transform.position.x < -10f)
        {
            transform.position = new Vector3(-10f, transform.position.y, transform.position.z);
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
