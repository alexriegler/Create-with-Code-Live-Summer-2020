using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.PlayerLoop;
using System;

public class SimpleCarController : MonoBehaviour
{
    [Header("Input")]
    public string verticalAxisName;
    public string horizontalAxisName;
    public string brakeButton;
    
    private float verticleInput;
    private float hoziontalInput;

    private bool brakesApplied = false;

    public Vehicle car;

    void Start()
    {
        car.SetCenterOfMass();
    }

    private void Update()
    {
        // Get driving input
        verticleInput = Input.GetAxis(verticalAxisName);
        hoziontalInput = Input.GetAxis(horizontalAxisName);

        // Check if brakes are being applied
        brakesApplied = Input.GetButton(brakeButton);
    }

    void FixedUpdate()
    {
        // TODO: Remove debug print
        print("Vw: " + car.GetWheelVelocity().ToString("f0") + "km/h   Vc: " + car.GetVehicleVelocity().ToString("f0") + "km/h   RPM: " + car.GetWheelRpm().ToString("f0"));

        car.AdjustSteering();

        GetDrivingInput();

        car.ControlTorque();

        car.Stabilize();

        car.ApplyTorque();

        if (brakesApplied)
        {
            car.ApplyBrakes();
        }
        else
        {
            car.ReleaseBrakes();
        }
    }

    // Methods

    // Get driving input
    void GetDrivingInput()
    {
        motorTorque = maxMotorTorque * verticleInput;
        steeringTorque = steeringAngle * hoziontalInput;
    }
}