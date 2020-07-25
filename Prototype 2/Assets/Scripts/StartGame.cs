using UnityEngine;

public class StartGame : MonoBehaviour
{
    public SpawnManager spawnManager;
    
    private Animal animal;

    // Start is called before the first frame update
    void Start()
    {
        animal = gameObject.GetComponent<Animal>();

        animal.OnFullFeed += () => spawnManager.gameObject.SetActive(true);
    }
}
