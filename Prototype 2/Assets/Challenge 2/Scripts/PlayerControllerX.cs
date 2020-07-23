using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;

    private float timeLastUsed = 0f;
    private float timeNextUse = 0f;
    private float timeCooldown = 1f;

    // Update is called once per frame
    void Update()
    {
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Time.time >= timeNextUse)
            {
                timeLastUsed = Time.time;
                Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
                timeNextUse = timeLastUsed + timeCooldown;
            }
        }
    }
}
