using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] players;
    public GameObject[] playerTexts;

    public float sceneLoadDelay = 10f;

    public const string WinningText = "You won!";
    public const string LosingText = "You lost";

    public void StartGame()
    {
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerCarController>().AllowInput = true;
        }
    }

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

        StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(sceneLoadDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
