using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Vehicle))]
public class AICarController : MonoBehaviour
{
    public enum AIBehavior
    {
        DriveForward,
        Stop
    }

    public AIBehavior behavior;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
