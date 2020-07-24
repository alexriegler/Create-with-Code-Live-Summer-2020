using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Player player;
    
    public int Score { get; private set; } = 0;

    private Text scoreText;

    private int feedPoints = 10;
    private int fullFeedPoints = 40;

    public event Action OnScoreChange;

    void Start()
    {
        scoreText = gameObject.GetComponent<Text>();
        scoreText.text = Score.ToString();

        OnScoreChange += () => scoreText.text = Score.ToString();
    }

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
