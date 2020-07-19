using UnityEngine;

[RequireComponent(typeof(Vehicle))]
public class PlayerCarController : MonoBehaviour
{
    [Header("Input")]
    public string verticalAxisName;
    public string horizontalAxisName;
    public string brakeButton;

    private float verticleInput;
    private float hoziontalInput;
    private bool allowInput = false;

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

    // Get and set whether to allow driving input
    public bool AllowInput
    {
        get
        {
            return allowInput;
        }
        set
        {
            allowInput = value;
        }
    }

    // Get driving input
    void GetDrivingInput()
    {
        if (allowInput)
        {
            car.MotorTorque = verticleInput;
            car.SteeringTorque = hoziontalInput;
        }
        else
        {
            car.MotorTorque = 0f;
            car.SteeringTorque = 0f;
        }
    }
}