using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - 50)
        {
            transform.position = startPos;
        }
    }
}
