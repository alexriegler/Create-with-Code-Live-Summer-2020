using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private float speed = 20;

    private PlayerController playerControllerScript;
    private float leftBound = -15;

    // Start is called before the first frame update
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }

        if (gameObject.CompareTag("Obstacle") && transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
