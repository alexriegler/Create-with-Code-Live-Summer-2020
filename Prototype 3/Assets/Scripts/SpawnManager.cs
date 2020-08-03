using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabs;

    private int obstacleIndex = 0;
    private float startDelay = 2;
    private float repeatRate = 2;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    private bool spawning = false;

    // Public methods

    // Starts the spawning of obstacles
    /// <summary>
    /// Starts the spawning of obstacles.
    /// </summary>
    public void StartSpawningObstacles()
    {
        if (!spawning)
        {
            InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
            spawning = true;
        }
    }

    // Stops the spawning of obstacles
    /// <summary>
    /// Stops the spawning of obstacles.
    /// </summary>
    public void StopSpawningObstacles()
    {
        if (spawning)
        {
            CancelInvoke(nameof(SpawnObstacle));
            spawning = false;
        }
    }

    // Private methods

    // Spawns an obstacle
    void SpawnObstacle()
    {
        obstacleIndex = Random.Range(0, obstaclePrefabs.Length);
        Instantiate(obstaclePrefabs[obstacleIndex], spawnPos, obstaclePrefabs[obstacleIndex].transform.rotation);
    }
}
