using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField]
    private Transform[] respawnPointArray;
    private bool[] respawnInUseArray;
    private int respawnPointIndex = 0;

    void Start()
    {
        respawnInUseArray = new bool[respawnPointArray.Length];
        for (int i = 0; i < respawnPointArray.Length; i++)
        {
            respawnInUseArray[i] = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if respawn point is being used
            bool spawnInUse = respawnInUseArray[respawnPointIndex];
            Transform respawnPoint;

            if (spawnInUse)
            {
                IncrementRespawnIndex();
            }

            respawnPoint = respawnPointArray[respawnPointIndex];

            StartCoroutine(RespawnPlayer(other.gameObject, respawnPoint));
        }
    }

    IEnumerator RespawnPlayer(GameObject player, Transform respawnPoint)
    {
        respawnInUseArray[respawnPointIndex] = true;
        IncrementRespawnIndex();

        player.transform.position = respawnPoint.transform.position;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.transform.eulerAngles = Vector3.forward;

        yield return new WaitForSeconds(1);

        player.GetComponent<Rigidbody>().isKinematic = false;
        respawnInUseArray[respawnPointIndex] = false;
    }

    private void IncrementRespawnIndex()
    {
        respawnPointIndex = (respawnPointIndex + 1) % respawnPointArray.Length;
    }
}
