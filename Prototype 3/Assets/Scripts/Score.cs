﻿using UnityEngine;

public class Score : MonoBehaviour
{
    public int PlayerScore { get; private set; }

    private ScrollManager scrollManager;
    private PlayerController player;
    private float distanceTraveled;
    private float scoreMultiplier;
    private readonly float defaultMultiplier = 1;
    private readonly float doubleMultiplier = 2;

    // Gets required object
    void Start()
    {
        scoreMultiplier = defaultMultiplier;
        
        scrollManager = FindObjectOfType<ScrollManager>();
        player = FindObjectOfType<PlayerController>();

        // Sets score multiplier to double points when player is dashing
        player.OnPlayerStartDash += () => scoreMultiplier = doubleMultiplier;
        // Sets score multiplier to normal points when player is finished dashing
        player.OnPlayerEndDash += () => scoreMultiplier = defaultMultiplier;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    // Updates the score
    void UpdateScore()
    {
        distanceTraveled += scrollManager.ScrollSpeed * scoreMultiplier * Time.deltaTime;
        PlayerScore = Mathf.RoundToInt(distanceTraveled);
        print(PlayerScore);
    }
}
