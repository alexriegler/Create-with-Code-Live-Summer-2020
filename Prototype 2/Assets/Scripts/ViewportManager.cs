using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewportManager : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField]
    private Camera gameCamera;

    // Start is called before the first frame update
    void Start()
    {
        gameCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
