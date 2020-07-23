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
}
