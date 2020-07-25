using System;
using UnityEngine;

public class Player : LivingEntity
{
    private int hitDamage = 1;

    public event Action OnPlayerHit;
    public event Action OnPlayerDeath;

    // When another collider triggers the player, the player takes damage
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeHit(hitDamage);
        }
    }

    public override void TakeHit(int damage)
    {
        bool wasAlive = !dead;
        base.TakeHit(damage);
        if (wasAlive)
        {
            OnPlayerHit?.Invoke();
        }
    }

    // Invokes the OnPlayerDeath action before calling the base Die method
    protected override void Die()
    {
        OnPlayerDeath?.Invoke();
        base.Die();
    }
}
