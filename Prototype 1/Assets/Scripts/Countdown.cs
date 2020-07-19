using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{
    public int countdownTime;
    public Text countdownText;
    public Text[] controlsTexts;
    public GameManager gameManager;

    private string goString = "GO!";

    private void Start()
    {
        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while(countdownTime > 0)
        {
            countdownText.text = countdownTime.ToString();

            yield return new WaitForSeconds(1f);

            countdownTime--;
        }

        countdownText.text = goString;

        gameManager.StartGame();

        yield return new WaitForSeconds(1f);

        countdownText.gameObject.SetActive(false);

        // Disable controls display
        for (int i = 0; i < controlsTexts.Length; i++)
        {
            controlsTexts[i].gameObject.SetActive(false);
        }
    }
}
