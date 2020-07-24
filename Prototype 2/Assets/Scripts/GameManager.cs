﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool gameHasEnded = false;

    public void EndGame()
    {
        if (!gameHasEnded)
        {
            print("Game Over");
        }
    }

    private void Restart()
    {
        print("Restart");
    }
}
