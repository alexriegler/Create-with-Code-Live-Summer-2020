using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    public Canvas gameOverCanvas;

    private bool gameHasEnded = false;

    void Start()
    {
        // Hide cursor
        Cursor.visible = false;

        player.OnPlayerDeath += EndGame;
    }

    public void EndGame()
    {
        gameOverCanvas.enabled = true;
        
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
