using UnityEngine;

public class Speedometer : MonoBehaviour
{
    public Vehicle car;

    // Update is called once per frame
    void Update()
    {
        print(car.GetVehicleVelocity());
    }
}
