using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] playerTexts;

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
}
