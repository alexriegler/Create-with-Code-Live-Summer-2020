using System.Collections;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(RespawnPlayer(other.gameObject));
        }
    }

    IEnumerator RespawnPlayer(GameObject player)
    {
        player.transform.position = respawnPoint.transform.position;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.transform.eulerAngles = Vector3.forward;
        yield return new WaitForSeconds(1);
        player.GetComponent<Rigidbody>().isKinematic = false;
    }
}
