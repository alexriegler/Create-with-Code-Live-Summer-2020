using System;
using UnityEngine;

public class Player : LivingEntity
{
    public event Action OnPlayerHit;
    public event Action OnPlayerDeath;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        // Takes one life from the player when hit action occurs
        OnPlayerHit += () => health--;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            OnPlayerHit?.Invoke();
        }
    }

    public override void Die()
    {
        OnPlayerDeath?.Invoke();
        base.Die();
    }
}
