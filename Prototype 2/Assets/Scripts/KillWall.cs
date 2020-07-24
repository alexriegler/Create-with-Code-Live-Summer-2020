using UnityEngine;

public class KillWall : MonoBehaviour
{
    [SerializeField]
    private Player player;
    
    private bool damagePlayer = false;
    private int playerDamage = 1;

    // Destroys any object that triggers the collider and deals damage to the player if it is an animal and damagePlayer is true
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && damagePlayer)
        {
            player.TakeHit(playerDamage);
        }
        Destroy(other.gameObject);
    }
}
