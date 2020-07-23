using UnityEngine;

public class LivingEntity : MonoBehaviour, IDamageable
{
    public int startingHealth;
    protected int health;
    protected bool dead;

    public virtual void Start()
    {
        health = startingHealth;
    }

    public void TakeHit(int damage, RaycastHit hit)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        dead = true;
        Destroy(gameObject);
    }
}
