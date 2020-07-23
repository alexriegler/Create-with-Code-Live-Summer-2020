using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    public float Speed { get; set; } = 10f;
    public int Damage { get; set; } = 1;

    // Update is called once per frame
    void Update()
    {
        float moveDistance = Speed * Time.deltaTime;
        CheckCollisions(moveDistance);
        transform.Translate(Vector3.forward * moveDistance);
    }

    void CheckCollisions(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(ray, out RaycastHit hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
        {
            OnHitObject(hit);
        }
    }

    void OnHitObject(RaycastHit hit)
    {
        IDamageable damageableObject = hit.collider.GetComponent<IDamageable>();
        damageableObject?.TakeHit(Damage, hit);
        Destroy(gameObject);
    }
}
