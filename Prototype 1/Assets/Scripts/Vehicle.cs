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

[RequireComponent (typeof (Rigidbody))]
public class Vehicle : MonoBehaviour
{
    // Car wheels
    [Header("Wheel list")]
    public List<AxleInfo> axleInfos;

    [Header("RPM")]
    public float idealRPM = 100f;
    public float maxRPM = 1400f;

    [Header("Motor")]
    public float maxMotorTorque = 2000f;
    public float minMotorTorqueRatio = 0.1f;
    private float motorTorque;

    [Header("Steering")]
    public float maxSteeringAngle = 10f;
    public float minSteeringAngle = 2f;
    [SerializeField]
    private float steeringAngle;
    [SerializeField]
    private float steeringTorque;

    [Header("Braking")]
    public float brakeTorque = 1000f;
    public float brakeForce = 20000f;

    [Header("Anti-Roll")]
    public float antiRollStrength = 20000.0f;

    [Header("Center of Mass")]
    public Vector3 comOffset;
    public Rigidbody vehicle;

    // Properties

    // Get & set the steering torque
    public float MotorTorque
    {
        get
        {
            return motorTorque;
        }
        // Motor torque is set to a fraction of max motor torque, value is [0,1]
        set
        {
            motorTorque = maxMotorTorque * value;
        }
    }

    // Get & set the steering torque
    public float SteeringTorque
    {
        get
        {
            return steeringTorque;
        }
        // Steering torque is set to a fraction of steering angle, value is [0,1]
        set
        {
            steeringTorque = steeringAngle * value;
        }
    }

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

    // Set the center of mass of the vehicle to the offset location
    public void SetCenterOfMass()
    {
        vehicle.centerOfMass = comOffset;
    }

    // Finds the velocity of the vehicle based on its position & previous position
    public float GetVehicleVelocity()
    {
        Vector3 velocityVector = vehicle.velocity;
        float velocity = velocityVector.magnitude;

        // Check sign of the velocity
        if (Vector3.Dot(Vector3.forward, velocityVector) < 0)
        {
            velocity = -velocity;
        }

        return velocity;
    }

    // Returns the wheel velocity in km/h
    public float GetWheelVelocity()
    {
        WheelCollider wheel = axleInfos[0].rightWheel;
        return wheel.radius * Mathf.PI * wheel.rpm * 60.0f / 1000.0f;
    }

    // Returns the rotations per minute of the wheels
    public float GetWheelRpm()
    {
        return axleInfos[0].rightWheel.rpm;
    }

    // Linearly interpolates the steering angle from max to min based on speed
    public void AdjustSteering()
    {
        steeringAngle = Mathf.Lerp(maxSteeringAngle, minSteeringAngle, GetWheelVelocity() / MaxSpeed);
    }

    // Controls the amount of torque applied to the wheels dependent upon the rpm
    public void ControlTorque()
    {
        if (GetWheelRpm() < idealRPM)
        {
            // Apply limited torque while below ideal rpm and max torque while at ideal rpm
            motorTorque = Mathf.Lerp(motorTorque * minMotorTorqueRatio, motorTorque, GetWheelRpm() / idealRPM);
        }
        else
        {
            // Apply max torque at ideal rpm and zero torque at max rpm
            motorTorque = Mathf.Lerp(motorTorque, 0f, (GetWheelRpm() - idealRPM) / (maxRPM - idealRPM));
        }
    }

    // Apply force on the wheels to prevent tipping
    public void Stabilize()
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
                vehicle.AddForceAtPosition(leftWheel.transform.up * -antiRollForce, leftWheel.transform.position);
            }
            if (isGroundedRight)
            {
                vehicle.AddForceAtPosition(rightWheel.transform.up * antiRollForce, rightWheel.transform.position);
            }
        }
    }

    // Apply torque to each wheel
    public void ApplyTorque()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steeringTorque;
                axleInfo.rightWheel.steerAngle = steeringTorque;
            }
            if (axleInfo.motor)
            {
                axleInfo.leftWheel.motorTorque = motorTorque;
                axleInfo.rightWheel.motorTorque = motorTorque;
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
    public void ApplyBrakes()
    {
        bool isGrounded = false;

        foreach (AxleInfo axleInfo in axleInfos)
        {
            axleInfo.leftWheel.brakeTorque = brakeTorque;
            axleInfo.rightWheel.brakeTorque = brakeTorque;

            // Check if any of the wheels are on the ground
            if (axleInfo.leftWheel.isGrounded || axleInfo.rightWheel.isGrounded)
            {
                isGrounded = true;
            }
        }

        // Check if the vehicle is grounded
        if (isGrounded)
        {
            // TODO: Apply variable brake force dependent on speed
            // Apply force in opposite direction of travel
            vehicle.AddForce(vehicle.velocity.normalized * -brakeForce);
        }
    }

    // Sets brake torque to zero
    public void ReleaseBrakes()
    {
        foreach (AxleInfo axleInfo in axleInfos)
        {
            axleInfo.leftWheel.brakeTorque = 0.0f;
            axleInfo.rightWheel.brakeTorque = 0.0f;
        }
    }

    /*
    // Debug

    // Draw center of mass gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + transform.rotation * comOffset, 0.4f);
    }
    */
}