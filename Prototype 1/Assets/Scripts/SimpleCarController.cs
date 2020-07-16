using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Experimental.PlayerLoop;

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
    public float maxSteeringAngle;
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
        print("Speed: " + GetSpeed().ToString("f0") + "km/h\t RPM: " + GetRpm().ToString("f0"));

        float motorTorque;
        float steeringTorque;

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

    // Methods

    // Returns the wheel speed in km/h
    public float GetSpeed()
    {
        WheelCollider wheel = axleInfos[0].rightWheel;
        return wheel.radius * Mathf.PI * wheel.rpm * 60.0f / 1000.0f;
    }

    // Returns the rotations per minute of the wheels
    public float GetRpm()
    {
        return axleInfos [0].rightWheel.rpm;
    }

    // Get driving input
    void GetDrivingInput(out float motorTorque, out float steeringTorque)
    {
        motorTorque = maxMotorTorque * verticleInput;
        steeringTorque = maxSteeringAngle * hoziontalInput;
    }

    void ControlRpm(ref float motorTorque)
    {
        if (GetRpm() < idealRPM)
        {
            // Replace MAGIC number 10.0f
            motorTorque = Mathf.Lerp(motorTorque / 10.0f, motorTorque, GetRpm() / idealRPM);
        }
        else
        {
            // Apply max torque at ideal rpm and zero torque at max rpm
            motorTorque = Mathf.Lerp(motorTorque, 0.0f, (GetRpm() - idealRPM) / (maxRPM - idealRPM));
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