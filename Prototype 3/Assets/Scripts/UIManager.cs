using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startButton;
    public GameObject restartButton;

    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        restartButton.SetActive(false);

        gm = FindObjectOfType<GameManager>();
        gm.OnGameOver += () => restartButton.SetActive(true);
    }
}
