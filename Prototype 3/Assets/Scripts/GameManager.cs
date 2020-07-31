using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private bool gameStarted = false;
    private bool gameOver = false;

    // TODO: Do I need these?
    public event Action OnGameStart;
    public event Action OnGameOver;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        player.OnPlayerDeath += EndGame;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Starts the game
    void StartGame()
    {
        print("Game Start");
        OnGameStart?.Invoke();
    }

    // Ends the game
    void EndGame()
    {
        print("Game Over");
        gameOver = true;
        OnGameOver?.Invoke();
    }
}
