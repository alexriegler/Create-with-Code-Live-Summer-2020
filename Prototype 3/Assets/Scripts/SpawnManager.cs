using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;

    private float startDelay = 2;
    private float repeatRate = 2;

    private Vector3 spawnPos = new Vector3(25, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawns an obstacle
    void SpawnObstacle()
    {
        Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
    }
}
