using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] animals;
    [SerializeField]
    private int animalIndex;

    [SerializeField]
    private float screenOffset;

    [SerializeField]
    private KeyCode spawnKey = KeyCode.S;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(spawnKey))
        {
            animalIndex = Random.Range(0, animals.Length);

            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            float screenTop = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, viewPos.z)).z;
            screenOffset = animals[animalIndex].transform.localScale.z;

            Instantiate(animals[animalIndex], new Vector3(Random.Range(-20, 20), 0f, screenTop + screenOffset), animals[animalIndex].transform.rotation);
        }
    }
}
