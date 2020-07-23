using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private ViewportManager vpManager;

    [SerializeField]
    private GameObject[] animals;
    [SerializeField]
    private int animalIndex;
    [SerializeField]
    private float startDelay = 2f;
    [SerializeField]
    private float spawnInterval = 1.5f;

    [SerializeField]
    private float screenOffset;

    // Start is called before the first frame update
    void Start()
    {        
        InvokeRepeating(nameof(SpawnRandomAnimal), startDelay, spawnInterval);
        InvokeRepeating(nameof(SpawnRandomSideAnimal), startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Spawns a random animal from the top of the screen
    void SpawnRandomAnimal()
    {
        float screenTop = vpManager.WTopBorderZ;
        float randomXPos = Random.Range(vpManager.WLeftBorderX, vpManager.WRightBorderX);

        animalIndex = Random.Range(0, animals.Length);
        screenOffset = animals[animalIndex].transform.localScale.z;

        Vector3 spawnPos = new Vector3(randomXPos, 0f, screenTop + screenOffset);

        Instantiate(animals[animalIndex], spawnPos, animals[animalIndex].transform.rotation);
    }

    // Spawns a random animal from one of the sides of the screen
    void SpawnRandomSideAnimal()
    {
        Vector3 spawnPos;
        Quaternion rotation;

        float screenLeft = vpManager.WLeftBorderX;
        float screenRight = vpManager.WRightBorderX;
        float randomZPos = Random.Range(vpManager.WBottomBorderZ, vpManager.WTopBorderZ);

        animalIndex = Random.Range(0, animals.Length);
        screenOffset = animals[animalIndex].transform.localScale.x;

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

        Instantiate(animals[animalIndex], spawnPos, rotation);
    }
}
