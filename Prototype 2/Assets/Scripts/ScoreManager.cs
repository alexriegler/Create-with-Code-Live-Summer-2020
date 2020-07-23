using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    
    public int Score { get; private set; } = 0;

    private int feedPoints = 10;
    private int fullFeedPoints = 40;

    public event Action OnScoreChange;

    public void AddAnimal(Animal animal)
    {
        animal.OnFeed += () => AddPoints(feedPoints);
        animal.OnFullFeed += () => AddPoints(fullFeedPoints);
    }

    // Increases the score by points
    void AddPoints(int points)
    {
        Score += points;
        OnScoreChange?.Invoke();
    }

    // Decreases the score by penalty
    void SubtractPoints(int penalty)
    {
        Score -= penalty;
        OnScoreChange?.Invoke();
    }
}
