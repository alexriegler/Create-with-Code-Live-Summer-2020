using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (!gameHasEnded)
        {
            gameCanvas.SetActive(false);
            gameOverCanvas.SetActive(true);

            // Show cursor
            Cursor.visible = true;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
