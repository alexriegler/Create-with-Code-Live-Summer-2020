using System;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // Public property
    public int Score { get; private set; } = 0;

    // Private variables
    private Text scoreText;
    private int feedPoints = 10;
    private int fullFeedPoints = 40;

    // Public events
    public event Action OnScoreChange;

    // Private methods
    void Start()
    {
        scoreText = gameObject.GetComponent<Text>();
        scoreText.text = Score.ToString();
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

    // Public methods

    // Subscribes to the animal's OnFeed and OnFullFeed actions
    public void AddAnimal(Animal animal)
    {
        animal.OnFeed += () => AddPoints(feedPoints);
        animal.OnFullFeed += () => AddPoints(fullFeedPoints);
    }

    // Updates the score text to reflect the current score
    public void UpdateScore() => scoreText.text = Score.ToString();
}
