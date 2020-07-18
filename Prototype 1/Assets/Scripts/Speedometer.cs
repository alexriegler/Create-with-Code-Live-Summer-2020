using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public const float KmhToMph = 0.6213711922f;

    public Vehicle car;
    public Text speedText;

    // Update is called once per frame
    void Update()
    {
        speedText.text = (car.GetVehicleVelocity() * KmhToMph).ToString("f0") + " mph";
    }
}
