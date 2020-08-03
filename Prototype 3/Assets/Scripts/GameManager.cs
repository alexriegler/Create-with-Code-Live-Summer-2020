using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Public properties
    public bool GameStarted { get; private set; } = false;
    public bool GameOver { get; private set; } = false;

    // Private variables
    private PlayerController player;

    // Public events
    public event Action OnIntroFinished;
    public event Action OnGameStart;
    public event Action OnGameOver;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        player.OnPlayerFinishWalkIn += () => OnIntroFinished?.Invoke();
        player.OnPlayerDeath += EndGame;
    }

    // Starts the game
    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
        print("Game Start");

        OnGameStart?.Invoke();
    }

    // Ends the game
    /// <summary>
    /// Ends the game.
    /// </summary>
    public void EndGame()
    {
        if (!GameOver)
        {
            GameOver = true;

            print("Game Over");

            OnGameOver?.Invoke();
        }
    }
}
