using UnityEngine;
using UnityEngine.UI;

public class HungerBar : MonoBehaviour
{
    private Slider hungerBar;
    private Animal animal;

    // Start is called before the first frame update
    void Start()
    {
        hungerBar = GetComponent<Slider>();
        animal = GetComponentInParent<Animal>();

        hungerBar.transform.localScale = new Vector3(hungerBar.transform.localScale.x * animal.startingHealth, hungerBar.transform.localScale.y, hungerBar.transform.localScale.z);

        hungerBar.maxValue = animal.startingHealth;
        hungerBar.value = animal.startingHealth;

        animal.OnFeed += () => hungerBar.value--;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
