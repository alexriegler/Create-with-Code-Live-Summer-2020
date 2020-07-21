using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] animals;

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
            Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
            float z = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, viewPos.z)).z;
            screenOffset = animals[0].transform.localScale.z;

            Instantiate(animals[0], new Vector3(0f, 0f, z + screenOffset), animals[0].transform.rotation);
        }
    }
}
