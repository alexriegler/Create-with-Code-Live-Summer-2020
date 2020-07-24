using System;
using UnityEngine;

public class Animal : LivingEntity
{
    [SerializeField]
    private float speed = 40.0f;

    public event Action OnFeed;
    public event Action OnFullFeed;

    // Moves the animals forward
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    // Called when the animal is fed
    public override void TakeHit(int damage)
    {
        OnFeed?.Invoke();
        base.TakeHit(damage);
    }

    // Called when the animal's hunger is satiated
    protected override void Die()
    {
        OnFullFeed?.Invoke();
        base.Die();
    }
}
