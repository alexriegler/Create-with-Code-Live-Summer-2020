using UnityEngine;

public class HungerBar : MonoBehaviour
{
    private Animal animal;

    // Start is called before the first frame update
    void Start()
    {
        animal = GetComponentInParent<Animal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
