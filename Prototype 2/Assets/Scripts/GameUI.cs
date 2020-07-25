using UnityEngine;

public class GameUI : MonoBehaviour
{
    public Player player;
    public PlayerHealth playerHealth;
    public ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        player.OnPlayerHit += DrawPlayerHealth;
        scoreManager.OnScoreChange += DrawScore;
    }

    void DrawPlayerHealth()
    {
        playerHealth.UpdateHealth();
    }

    void DrawScore()
    {
        print($"Score = {scoreManager.Score}");
    }
}
