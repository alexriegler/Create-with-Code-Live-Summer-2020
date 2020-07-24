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
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeHit(hitDamage);
            OnPlayerHit?.Invoke();
        }
    }

    protected override void Die()
    {
        OnPlayerDeath?.Invoke();
        base.Die();
    }
}
