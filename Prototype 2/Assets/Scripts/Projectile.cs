using UnityEngine;

public class Projectile : MonoBehaviour
{
    public LayerMask collisionMask;
    public float Speed { get; set; } = 10f;
    public float Damage { get; set; } = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

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
        GameObject hitObject = hit.collider.gameObject;
        Destroy(gameObject);
    }
}
