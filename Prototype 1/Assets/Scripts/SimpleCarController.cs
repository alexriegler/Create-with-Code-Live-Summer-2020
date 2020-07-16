using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.PlayerLoop;
using System;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class SimpleCarController : MonoBehaviour
{
    // Car wheels
    [Header("Wheel list")]
    public List<AxleInfo> axleInfos;
    
    [Header("RPM")]
    public float idealRPM = 500f;
    public float maxRPM = 1000f;
    
    [Header("Torque, Steering, & Braking")]
    public float maxMotorTorque;
    // TODO: Change to vector2d?
    public float maxSteeringAngle;
    public float minSteeringAngle;
    [SerializeField]
    private float steeringAngle;
    public float brakeTorque;
    public float brakeForce;

    private bool brakesApplied = false;

    [Header("Anti-Roll")]
    public float antiRollStrength = 20000.0f;

    [Header("Center of Mass")]
    public Vector3 comOffset;
    public Rigidbody rb;

    [Header("Input")]
    public string verticalAxisName;
    public string horizontalAxisName;
    public string brakeButton;
    
    private float verticleInput;
    private float hoziontalInput;

    void Start()
    {
        // Set the center of mass of the car to the offset
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = comOffset;
    }

    private void Update()
    {
        // Get driving input
        verticleInput = Input.GetAxis(verticalAxisName);
        hoziontalInput = Input.GetAxis(horizontalAxisName);

        // Check if brakes are being applied
        brakesApplied = Input.GetButton(brakeButton) ? true : false;
    }

    void FixedUpdate()
    {
        // TODO: Remove debug print
        print("Speed: " + GetWheelVelocity().ToString("f0") + "km/h\t RPM: " + GetWheelRpm().ToString("f0"));

        float motorTorque;
        float steeringTorque;

        AdjustSteering();

        GetDrivingInput(out motorTorque, out steeringTorque);

        ControlRpm(ref motorTorque);

        Stabilize();

        ApplyTorque(motorTorque, steeringTorque);

        if (brakesApplied)
        {
            ApplyBrakes();
        }
        else
        {
            ReleaseBrakes();
        }
    }

    // Properties

    // Get the maximum speed of the vehicle
    public float MaxSpeed
    {
        get
        {
            WheelCollider wheel = axleInfos[0].rightWheel;
            return wheel.radius * Mathf.PI * maxRPM * 60.0f / 1000.0f;
        }
    }

    // Methods

    // Returns the wheel velocity in km/h
    public float GetWheelVelocity()
    {
        WheelCollider wheel = axleInfos[0].rightWheel;
        return wheel.radius * Mathf.PI * wheel.rpm * 60.0f / 1000.0f;
    }

    // Returns the rotations per minute of the wheels
    public float GetWheelRpm()
    {
        return axleInfos [0].rightWheel.rpm;
    }

    // Linearly interpolates the steering angle from max to min based on speed
    void AdjustSteering()
    {
        steeringAngle = Mathf.Lerp(maxSteeringAngle, minSteeringAngle, GetWheelVelocity() / MaxSpeed);
    }

    // Get driving input
    void GetDrivingInput(out float motorTorque, out float steeringTorque)
    {
        motorTorque = maxMotorTorque * verticleInput;
        steeringTorque = steeringAngle * hoziontalInput;
    }

    void ControlRpm(ref float motorTorque)
    {
        if (GetWheelRpm() < idealRPM)
        {
            // Replace MAGIC number 10.0f
            motorTorque = Mathf.Lerp(motorTorque / 10.0f, motorTorque, GetWheelRpm() / idealRPM);
        }
        else
        {
            // Apply max torque at ideal rpm and zero torque at max rpm
            motorTorque = Mathf.Lerp(motorTorque, 0.0f, (GetWheelRpm() - idealRPM) / (maxRPM - idealRPM));
        }
    }

    // Apply force on the wheels to prevent tipping
    void Stabilize()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            WheelCollider leftWheel = axleInfo.leftWheel;
            WheelCollider rightWheel = axleInfo.rightWheel;
            WheelHit wheelHit;
            float leftTravel = 1.0f;
            float rightTravel = 1.0f;

            bool isGroundedLeft = leftWheel.GetGroundHit(out wheelHit);
            if (isGroundedLeft)
            {
                leftTravel = (-leftWheel.transform.InverseTransformPoint(wheelHit.point).y - leftWheel.radius) / leftWheel.suspensionDistance;
            }

            bool isGroundedRight = rightWheel.GetGroundHit(out wheelHit);
            if (isGroundedRight)
            {
                rightTravel = (-rightWheel.transform.InverseTransformPoint(wheelHit.point).y - rightWheel.radius) / rightWheel.suspensionDistance;
            }

            float antiRollForce = (leftTravel - rightTravel) * antiRollStrength;

            if (isGroundedLeft)
            {
                rb.AddForceAtPosition(leftWheel.transform.up * -antiRollForce, leftWheel.transform.position);
            }
            if (isGroundedRight)
            {
                rb.AddForceAtPosition(rightWheel.transform.up * antiRollForce, rightWheel.transform.position);
            }
        }
    }

    // Apply torque to each wheel
    void ApplyTorque(float motor, float steering)
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }

    // Apply transforms to corresponding visual wheels
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    // Applies brake torque to each wheel and applies a brake force to the car
    void ApplyBrakes()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            axleInfo.leftWheel.brakeTorque = brakeTorque;
            axleInfo.rightWheel.brakeTorque = brakeTorque;
        }
        rb.AddForce(Vector3.back * brakeForce);
    }

    void ReleaseBrakes()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            axleInfo.leftWheel.brakeTorque = 0.0f;
            axleInfo.rightWheel.brakeTorque = 0.0f;
        }
    }

    // Debug

    // Draw center of mass gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * comOffset, 0.4f);
    }
}