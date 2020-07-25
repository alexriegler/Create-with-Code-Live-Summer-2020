using UnityEngine;

public class GameUI : MonoBehaviour
{
    public Player player;
    
    private PlayerHealth playerHealth;
    private ScoreManager scoreManager;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = gameObject.GetComponentInChildren<PlayerHealth>();
        scoreManager = gameObject.GetComponentInChildren<ScoreManager>();
        
        player.OnPlayerHit += () => playerHealth.UpdateHealth();
        scoreManager.OnScoreChange += () => scoreManager.UpdateScore();
    }
}
