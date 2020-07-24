using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public ViewportManager vpManager;
    public ScoreManager scoreManager;
    public GameObject[] animalPrefabs;

    public Vector2 secondsBetweenSpawnsMinMax;
    private float nextSpawnTime;

    private int animalIndex;
    private float screenOffset;

    // Start is called before the first frame update
    void Start()
    {        
        
    }

    void Update()
    {
        if (Time.time > nextSpawnTime)
        {
            float secondsBetweenSpawns = Mathf.Lerp(secondsBetweenSpawnsMinMax.y, secondsBetweenSpawnsMinMax.x, Difficulty.GetDifficultyPercent());
            nextSpawnTime = Time.time + secondsBetweenSpawns;

            SpawnRandomAnimal();
            SpawnRandomSideAnimal();
        }
    }

    // Spawns a random animal from the top of the screen
    void SpawnRandomAnimal()
    {
        float screenTop = vpManager.WTopBorderZ;
        float randomXPos = Random.Range(vpManager.WLeftBorderX, vpManager.WRightBorderX);

        animalIndex = Random.Range(0, animalPrefabs.Length);
        screenOffset = animalPrefabs[animalIndex].transform.localScale.z;

        Vector3 spawnPos = new Vector3(randomXPos, 0f, screenTop + screenOffset);

        GameObject spawnedAnimal = Instantiate(animalPrefabs[animalIndex], spawnPos, animalPrefabs[animalIndex].transform.rotation);

        scoreManager.AddAnimal(spawnedAnimal.GetComponent<Animal>());
    }

    // Spawns a random animal from one of the sides of the screen
    void SpawnRandomSideAnimal()
    {
        Vector3 spawnPos;
        Quaternion rotation;

        float screenLeft = vpManager.WLeftBorderX;
        float screenRight = vpManager.WRightBorderX;
        float randomZPos = Random.Range(vpManager.WBottomBorderZ, vpManager.WTopBorderZ);

        animalIndex = Random.Range(0, animalPrefabs.Length);
        screenOffset = animalPrefabs[animalIndex].transform.localScale.x;

        // TODO: Change to enum or something
        int randomSide = Random.Range(0, 2);

        // Left side = 0 or Right side = 1
        if (randomSide == 0)
        {
            spawnPos = new Vector3(screenLeft - screenOffset, 0f, randomZPos);
            rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
        }
        else
        {
            spawnPos = new Vector3(screenRight + screenOffset, 0f, randomZPos);
            rotation = Quaternion.Euler(new Vector3(0f, -90f, 0f));
        }

        GameObject spawnedAnimal = Instantiate(animalPrefabs[animalIndex], spawnPos, rotation);

        scoreManager.AddAnimal(spawnedAnimal.GetComponent<Animal>());
    }
}
