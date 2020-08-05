using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Background : MoveLeft
{
    private Vector3 startPos;
    private float repeatWidth;

    // Initializes required components and variables
    void Start()
    {
        startPos = transform.position;
        repeatWidth = GetComponent<BoxCollider>().size.x / 2;
    }

    // Controls the background movement
    void Update()
    {
        Move();
        RepeatBackground();
    }

    // Repositions the background to give the illusion of a never ending background
    void RepeatBackground()
    {
        if (transform.position.x < startPos.x - repeatWidth)
        {
            transform.position = startPos;
        }
    }
}
