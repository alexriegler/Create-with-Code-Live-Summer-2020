using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Lives { get; private set; } = 3;

    public event Action OnPlayerHit;
    public event Action OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        // Takes one life from the player when hit
        OnPlayerHit += () => Lives--;
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
            OnPlayerHit?.Invoke();
        }
    }
}
