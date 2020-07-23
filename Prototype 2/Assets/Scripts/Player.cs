using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Lives { get; private set; } = 3;

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
        if (Lives < 1)
        {
            OnPlayerDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnTakeDamage?.Invoke();
        }
    }

    // Takes one life from the player
    void TakeDamage()
    {
        Lives--;
    }
}
