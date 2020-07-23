using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int lives = 3;
    public event Action OnTakeDamage;
    public event Action OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy the player on falling to 0 lives
        if (lives < 1)
        {
            OnPlayerDeath();
            Destroy(gameObject);
        }
    }
}
