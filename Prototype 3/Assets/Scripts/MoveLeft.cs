using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    private ScrollManager scrollManager;

    // Get scroll manager
    void Awake()
    {
        scrollManager = FindObjectOfType<ScrollManager>();
    }

    // Moves the game object left
    protected void Move()
    {
        transform.Translate(Vector3.left * scrollManager.ScrollSpeed * Time.deltaTime);
    }
}
