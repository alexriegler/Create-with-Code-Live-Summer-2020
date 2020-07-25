using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Toggle[] hearts;

    private Stack<Toggle> heartStack;

    // Start is called before the first frame update
    void Start()
    {
        heartStack = new Stack<Toggle>(hearts.Length);

        foreach (Toggle heart in hearts)
        {
            heartStack.Push(heart);
        }
    }

    // Updates the health visual
    public void UpdateHealth()
    {
        if (heartStack.Peek() != null)
        {
            Toggle heart = heartStack.Pop();
            heart.isOn = false;
        }
    }
}
