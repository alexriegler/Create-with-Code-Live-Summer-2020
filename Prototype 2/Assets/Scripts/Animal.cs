using System;
using UnityEngine;

public class Animal : LivingEntity
{
    public event Action OnFeed;
    public event Action OnFullFeed;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
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
