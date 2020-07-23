using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    
    public int Score { get; private set; } = 0;

    private int feedPoints = 10;
    private int missPenalty = 15;
    private int hitPenalty = 20;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Increases the score by points
    void AddPoints(int points) => Score += points;

    // Decreases the score by penalty
    void SubtractPoints(int penalty) => Score -= penalty;

    // Increases the score by feed points
    void AddFeedPoints() => AddPoints(feedPoints);

    // Decreases the score by miss penalty
    void SubtractMissPenalty() => SubtractPoints(missPenalty);

    // Decreases the score by hit penalty
    void SubtractHitPenalty() => SubtractPoints(hitPenalty);
}
