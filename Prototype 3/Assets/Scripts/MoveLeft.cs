using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public ScrollManager scrollManager;

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
        // TODO: Remove player game over bool
        if (!playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * scrollManager.ScrollSpeed * Time.deltaTime);
        }

        if (gameObject.CompareTag("Obstacle") && transform.position.x < leftBound)
        {
            Destroy(gameObject);
        }
    }
}
