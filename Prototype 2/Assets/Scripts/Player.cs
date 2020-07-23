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
        OnTakeDamage += TakeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        // Destroy the player on falling to 0 lives
        if (lives < 1)
        {
            OnPlayerDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            print("Hit!");
        }
    }

    // Takes one life from the player
    void TakeDamage()
    {
        lives--;
    }
}
