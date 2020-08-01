using UnityEngine;

public class Score : MonoBehaviour
{
    public int PlayerScore { get; private set; }
    
    private float distanceTraveled;
    private ScrollManager scrollManager;

    // Gets required object
    void Start()
    {
        scrollManager = FindObjectOfType<ScrollManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateScore();
    }

    // Updates the score
    void UpdateScore()
    {
        distanceTraveled += scrollManager.ScrollSpeed * Time.deltaTime;
        PlayerScore = Mathf.RoundToInt(distanceTraveled);
        print(PlayerScore);
    }
}
