using UnityEngine;

public class StartGame : MonoBehaviour
{
    private Animal animal;

    // Start is called before the first frame update
    void Start()
    {
        animal = gameObject.GetComponent<Animal>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
