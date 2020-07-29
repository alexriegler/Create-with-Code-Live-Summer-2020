using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    
    public bool gameOver;

    [SerializeField] string jumpButton = "Jump";
    [SerializeField] float jumpForce = 10;
    [SerializeField] float gravityModifier = 1;

    private Rigidbody playerRb;
    private Animator playerAnim;
    private GameObject playerMesh; 
    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerMesh = GetComponentInChildren<SkinnedMeshRenderer>().gameObject;
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(jumpButton) && isGrounded && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            playerAnim.SetTrigger("Jump_trig");
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
            // playerAnim.SetBool("Death_b", true);
            // playerAnim.SetInteger("DeathType_int", 1);

            playerRb.isKinematic = true;
            playerMesh.SetActive(false);

            explosionParticle.Play();
        }
    }
}
