using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;

    private bool gameHasEnded = false;

    void Start()
    {
        player.OnPlayerDeath += EndGame;
    }

    public void EndGame()
    {
        Cursor.visible = true;

        if (!gameHasEnded)
        {
            print("Game Over");
        }
    }

    private void Restart()
    {
        print("Restart");
    }
}
