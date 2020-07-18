using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.transform.position = respawnPoint.transform.position;
        }
    }
}
