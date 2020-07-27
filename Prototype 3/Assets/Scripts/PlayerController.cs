using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] string jumpButton = "Jump";
    [SerializeField] float jumpForce = 10;
    [SerializeField] float gravityModifier = 1;


    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(jumpButton))
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
