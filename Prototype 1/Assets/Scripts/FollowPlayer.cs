using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    public KeyCode cycleCameraButton;
    public KeyCode rearViewButton;

    private Vector3 rearView = new Vector3(0, 5, 12);
    private Vector3 close = new Vector3(0, 5, -7);
    private Vector3 medium = new Vector3(0, 5, -9);
    private Vector3 far = new Vector3(0, 5, -11);
    private Vector3[] offsetArray = new Vector3[3];
    private Vector3 offset;

    private int offsetIndex = 0;

    private bool isRearView = false;

    // Start is called before the first frame update
    void Start()
    {
        offsetArray[0] = close;
        offsetArray[1] = medium;
        offsetArray[2] = far;
    }

    // Update is called once per frame
    void Update()
    {
        // Cycle through views with C key
        if (!isRearView)
        {
            if (Input.GetKeyDown(cycleCameraButton))
            {
                offsetIndex = (offsetIndex + 1) % 3;
            }
            offset = offsetArray[offsetIndex];
        }

        // While the R key is held, rear view
        if (Input.GetKeyDown(rearViewButton))
        {
            isRearView = true;
            offset = rearView;
            transform.Rotate(Vector3.up, 180.0f, Space.World);
        }

        if (Input.GetKeyUp(rearViewButton))
        {
            isRearView = false;
            transform.Rotate(Vector3.up, 180.0f, Space.World);
        }

        transform.position = player.transform.position + offset;
    }
}
