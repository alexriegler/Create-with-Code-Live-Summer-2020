using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] playerTexts;
    
    bool gameHasEnded = false;

    public float restartDelay = 1f;

    public const string WinningText = "You won!";
    public const string LosingText = "You lost";

    public void CompleteLevel(GameObject winningPlayer)
    {
        for (int i = 0; i < players.Length; i++)
        {
            playerTexts[i].SetActive(true);

            if (players[i].Equals(winningPlayer))
            {    
                playerTexts[i].GetComponent<Text>().text = WinningText;
            }
            else
            {
                players[i].GetComponent<PlayerCarController>().enabled = false;
                playerTexts[i].GetComponent<Text>().text = LosingText;
            }
        }
        print("Level Complete!");
    }

    public void EndGame()
    {
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", restartDelay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
