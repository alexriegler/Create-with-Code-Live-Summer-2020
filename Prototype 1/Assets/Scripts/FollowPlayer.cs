using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;

    private Vector3 close = new Vector3(0, 5, -7);
    private Vector3 medium = new Vector3(0, 5, -9);
    private Vector3 far = new Vector3(0, 5, -11);
    private Vector3[] offsets = new Vector3[3];

    private int offsetIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        offsets[0] = close;
        offsets[1] = medium;
        offsets[2] = far;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            print(offsetIndex);
            offsetIndex = (offsetIndex + 1) % 3;
            print(offsetIndex);
        }
        transform.position = player.transform.position + offsets[offsetIndex];
    }
}
