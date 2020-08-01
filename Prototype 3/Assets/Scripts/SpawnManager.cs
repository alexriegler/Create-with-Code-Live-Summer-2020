using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    private int obstacleIndex = 0;
    private float startDelay = 2;
    private float repeatRate = 2;
    private Vector3 spawnPos = new Vector3(25, 0, 0);

    // Start spawning obstacles
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
    }

    // Spawns an obstacle
    void SpawnObstacle()
    {
        obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);
    }

    // Stop spawning obstacles
    public void StopSpawningObstacles()
    {
        CancelInvoke(nameof(SpawnObstacle));
    }
}
