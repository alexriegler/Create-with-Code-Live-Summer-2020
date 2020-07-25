using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public GameObject gameCanvas;
    public GameObject gameOverCanvas;

    private bool gameHasEnded = false;

    void Start()
    {
        // Hide cursor
        Cursor.visible = false;

        player.OnPlayerDeath += EndGame;
    }

    public void EndGame()
    {
        gameCanvas.SetActive(false);
        gameOverCanvas.SetActive(true);

        // Show cursor
        Cursor.visible = true;

        if (!gameHasEnded)
        {
            print("Game Over");
        }
    }

    public void Restart()
    {
        print("Restart");
    }
}
