using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Public properties
    public bool GameOver { get; private set; } = false;

    // Private variables
    private PlayerController player;

    // Public events
    public event Action OnIntroFinished;
    public event Action OnGameStart;
    public event Action OnGameOver;
    public event Action OnGameRestart;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        // Calls the OnIntroFinished event when the player finishes their walk in
        player.OnPlayerFinishWalkIn += () => OnIntroFinished?.Invoke();
        // Ends the game when the player dies
        player.OnPlayerDeath += EndGame;
    }

    // Starts the game
    /// <summary>
    /// Starts the game.
    /// </summary>
    public void StartGame()
    {
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
            OnGameOver?.Invoke();
        }
    }

    // Restarts the game by reloading the current scene
    /// <summary>
    /// Restarts the game.
    /// </summary>
    public void RestartGame()
    {
        GameOver = false;
        OnGameRestart?.Invoke();
        StartGame();
    }
}
