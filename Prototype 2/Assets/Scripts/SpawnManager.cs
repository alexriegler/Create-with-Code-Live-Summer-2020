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
        animalIndex = Random.Range(0, animals.Length);

        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        float screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, viewPos.z)).z;
        screenOffset = animals[animalIndex].transform.localScale.z;
        float randomXPos = Random.Range(vpManager.WLeftBorderX, vpManager.WRightBorderX);
        Vector3 spawnPos = new Vector3(randomXPos, 0f, screenTop + screenOffset);

        Instantiate(animals[animalIndex], spawnPos, animals[animalIndex].transform.rotation);
    }
}
