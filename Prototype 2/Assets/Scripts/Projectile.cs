using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float Speed { get; set; } = 40f;
    public int Damage { get; set; } = 1;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Speed * Time.deltaTime);
    }

    // When another collider triggers the projectile, if it's an Enemy, they take damage
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            IDamageable damageableObject = other.GetComponent<IDamageable>();
            damageableObject?.TakeHit(Damage);
            Destroy(gameObject);
        }
    }
}
