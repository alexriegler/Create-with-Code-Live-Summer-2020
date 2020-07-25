using UnityEngine;
using UnityEngine.UI;

public class FinalScore : MonoBehaviour
{
    public ScoreManager scoreManager;

    private Text finalScoreText;
    
    // Start is called before the first frame update
    void Start()
    {
        finalScoreText = gameObject.GetComponent<Text>();

        finalScoreText.text = scoreManager.Score.ToString();
    }
}
