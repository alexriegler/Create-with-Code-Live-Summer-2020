using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Vehicle car;
    public Text speedText;

    // Update is called once per frame
    void Update()
    {
        speedText.text = car.GetVehicleVelocity().ToString("f0") + " km/h";
    }
}
