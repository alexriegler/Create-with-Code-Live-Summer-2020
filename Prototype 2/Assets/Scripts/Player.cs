using System;
using UnityEngine;

public class Player : LivingEntity
{
    private int hitDamage = 1;

    public event Action OnPlayerHit;
    public event Action OnPlayerDeath;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();

        // Takes one life from the player when hit action occurs
        OnPlayerHit += () => TakeHit(hitDamage);
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

    protected override void Die()
    {
        OnPlayerDeath?.Invoke();
        base.Die();
    }
}
