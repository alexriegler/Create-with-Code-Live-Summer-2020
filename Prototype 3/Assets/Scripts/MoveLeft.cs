using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public ScrollManager scrollManager;

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
