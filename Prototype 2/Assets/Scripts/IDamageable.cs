using UnityEngine;

public interface IDamageable
{
    // Object takes damage from hit equal to damage
    void TakeHit(int damage, RaycastHit hit);
    void TakeHit(int damage);
}