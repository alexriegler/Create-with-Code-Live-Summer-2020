using UnityEngine;

public class Score : MonoBehaviour
{
    public int PlayerScore { get; private set; }
    private float distanceTraveled;

    private ScrollManager scrollManager;

    // Start is called before the first frame update
    void Start()
    {
        scrollManager = FindObjectOfType<ScrollManager>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceTraveled += scrollManager.ScrollSpeed * Time.deltaTime;
        PlayerScore = Mathf.RoundToInt(distanceTraveled);
        print(PlayerScore);
    }
}
