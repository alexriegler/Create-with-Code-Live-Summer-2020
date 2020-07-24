using UnityEngine;

public class GameUI : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        DrawPlayerHealth(player.startingHealth);
        DrawScore();

        player.OnPlayerHit += () => DrawPlayerHealth(player.Health);
        scoreManager.OnScoreChange += DrawScore;
    }

    // Update is called once per frame
    void Update()
    {
        
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
