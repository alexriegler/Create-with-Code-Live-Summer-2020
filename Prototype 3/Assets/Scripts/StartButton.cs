using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    private GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        // Set button to not interactable
        SetButtonInteractable(false);

        gm = FindObjectOfType<GameManager>();
        gm.OnIntroFinished += () => SetButtonInteractable(true);
    }

    // Sets the interactability of the button to be val
    void SetButtonInteractable(bool val)
    {
        gameObject.GetComponent<Button>().interactable = val;
    }
}
