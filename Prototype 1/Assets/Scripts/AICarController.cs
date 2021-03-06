﻿using System.Collections;
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
    public float maxSpeed = 25f;

    // Start is called before the first frame update
    void Start()
    {
        car.SetCenterOfMass();
    }

    void FixedUpdate()
    {
        if (behavior == AIBehavior.DriveForward)
        {
            if (car.GetVehicleVelocity() < maxSpeed)
            {
                car.AdjustSteering();

                car.MotorTorque = 1f;

                car.ControlTorque();

                car.Stabilize();

                car.ApplyTorque();
            }
        }
    }
}
