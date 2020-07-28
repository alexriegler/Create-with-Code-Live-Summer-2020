using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool gameOver;

    [SerializeField] string jumpButton = "Jump";
    [SerializeField] float jumpForce = 10;
    [SerializeField] float gravityModifier = 1;

    private Rigidbody playerRb;
    private Animator playerAnim;
    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(jumpButton) && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            print("Game Over");
            gameOver = true;
        }
    }
}
