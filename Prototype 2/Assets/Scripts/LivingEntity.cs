using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public int startingHealth;
    public int Health { get; protected set; }
    protected bool dead;

    protected virtual void Start()
    {
        Health = startingHealth;
    }

    public virtual void TakeHit(int damage, RaycastHit hit)
    {
        Health -= damage;

        if (Health <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void TakeHit(int damage)
    {
        Health -= damage;

        if (Health <= 0 && !dead)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        dead = true;
        Destroy(gameObject);
    }
}
