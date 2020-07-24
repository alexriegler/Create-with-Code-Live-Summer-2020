using UnityEngine;

public class GameUI : MonoBehaviour
{
    public Player player;
    public ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        DrawPlayerHealth(player.startingHealth);
        DrawScore();

        player.OnPlayerHit += () => DrawPlayerHealth(player.Health);
        scoreManager.OnScoreChange += DrawScore;
    }

    void DrawPlayerHealth(int health)
    {
        print($"Lives = {health}");
    }

    void DrawScore()
    {
        print($"Score = {scoreManager.Score}");
    }
}
