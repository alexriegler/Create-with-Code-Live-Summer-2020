using UnityEngine;

[RequireComponent (typeof (Vehicle))]
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
        verticleInput = Input.GetAxisRaw(verticalAxisName);
        hoziontalInput = Input.GetAxisRaw(horizontalAxisName);

        // Check if brakes are being applied
        brakesApplied = Input.GetButton(brakeButton);
    }

    void FixedUpdate()
    {
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
        car.MotorTorque = verticleInput;
        car.SteeringTorque = hoziontalInput;
    }
}