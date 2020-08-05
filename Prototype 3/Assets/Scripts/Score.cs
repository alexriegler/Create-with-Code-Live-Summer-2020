using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public int PlayerScore { get; private set; }

    private GameManager gm;
    private ScrollManager scrollManager;
    private PlayerController player;
    private Text scoreText;
    private float distanceTraveled;
    private float scoreMultiplier;
    private readonly float defaultMultiplier = 1;
    private readonly float doubleMultiplier = 2;

    // Gets required object
    void Start()
    {
        scoreMultiplier = defaultMultiplier;

        gm = FindObjectOfType<GameManager>();
        gm.OnGameRestart += ResetScore;

        scrollManager = FindObjectOfType<ScrollManager>();
        
        player = FindObjectOfType<PlayerController>();
        // Sets score multiplier to double points when player is dashing
        player.OnPlayerStartDash += () => scoreMultiplier = doubleMultiplier;
        // Sets score multiplier to normal points when player is finished dashing
        player.OnPlayerEndDash += () => scoreMultiplier = defaultMultiplier;

        scoreText = GetComponent<Text>();
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
        scoreText.text = PlayerScore.ToString("N0");
    }

    // Resets the score
    void ResetScore()
    {
        PlayerScore = 0;
        distanceTraveled = 0;
        scoreMultiplier = defaultMultiplier;
    }
}
