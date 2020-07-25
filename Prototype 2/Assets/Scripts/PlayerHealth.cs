using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Toggle[] hearts;

    private Stack heartStack;

    // Start is called before the first frame update
    void Start()
    {
        heartStack = new Stack();

        foreach (Toggle heart in hearts)
        {
            heartStack.Push(heart);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
