using System;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public event Action OnGameRestart;

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
        OnGameRestart?.Invoke();

        // bring player to origin/start
        //    - start running immediately
        //    - bool dead should be set to false
        //    - revive method?
        // reset background
        // remove obstacles and start spawning again
        // restart button deactivated
        // score reset
        //     - keep track of previous scores?
        // 
    }
}
