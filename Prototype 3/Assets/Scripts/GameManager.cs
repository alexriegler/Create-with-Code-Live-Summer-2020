using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerController player;
    private SpawnManager spawnManager;
    private ScrollManager scrollManager;
    public bool GameStarted { get; private set; } = false;
    public bool GameOver { get; private set; } = false;

    // TODO: Do I need these?
    public event Action OnGameStart;
    public event Action OnGameOver;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        spawnManager = FindObjectOfType<SpawnManager>();
        scrollManager = FindObjectOfType<ScrollManager>();

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
        if (!GameOver)
        {
            GameOver = true;

            print("Game Over");

            OnGameOver?.Invoke();
        }
    }
}
