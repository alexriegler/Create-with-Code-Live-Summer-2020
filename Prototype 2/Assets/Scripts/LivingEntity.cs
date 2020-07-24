using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public int startingHealth;
    public int Health { get; protected set; }
    protected bool dead;

    // Initializes the living entity's health to starting health
    protected virtual void Start()
    {
        Health = startingHealth;
    }

    // Takes health away from the living entity equal to damage
    /// <summary>
    /// Deals damage to the health of the living entity.
    /// </summary>
    /// <param name="damage">The amount of health to take away from the living entity.</param>
    public virtual void TakeHit(int damage)
    {
        Health -= damage;

        if (Health <= 0 && !dead)
        {
            Die();
        }
    }

    // Sets dead to true and destroys the living entity
    /// <summary>
    /// Destroys the living entity.
    /// </summary>
    protected virtual void Die()
    {
        dead = true;
        Destroy(gameObject);
    }
}
