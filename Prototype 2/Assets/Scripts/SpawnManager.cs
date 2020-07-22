using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    private KeyCode spawnKey = KeyCode.S;

    // Start is called before the first frame update
    void Start()
    {        
        InvokeRepeating("SpawnRandomAnimal", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnRandomAnimal()
    {
        float screenTop = vpManager.WTopBorderZ;
        float randomXPos = Random.Range(vpManager.WLeftBorderX, vpManager.WRightBorderX);

        animalIndex = Random.Range(0, animals.Length);
        screenOffset = animals[animalIndex].transform.localScale.z;

        Vector3 spawnPos = new Vector3(randomXPos, 0f, screenTop + screenOffset);

        Instantiate(animals[animalIndex], spawnPos, animals[animalIndex].transform.rotation);
    }
}
