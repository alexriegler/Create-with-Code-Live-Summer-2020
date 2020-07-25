using UnityEngine;

public class StartGame : MonoBehaviour
{
    public SpawnManager spawnManager;
    public ScoreManager scoreManager;
    
    private Animal animal;

    // Start is called before the first frame update
    void Start()
    {
        animal = gameObject.GetComponent<Animal>();

        scoreManager.AddAnimal(animal);

        animal.OnFullFeed += () => spawnManager.gameObject.SetActive(true);
    }
}
