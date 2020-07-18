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
    public Vehicle car;

    // Start is called before the first frame update
    void Start()
    {
        car.SetCenterOfMass();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
