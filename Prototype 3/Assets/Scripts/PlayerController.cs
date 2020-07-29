using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public float jumpVolume = 1.0f;
    public AudioClip crashSound;
    public float crashVolume = 1.0f;

    public bool gameOver;

    [SerializeField] string jumpButton = "Jump";
    [SerializeField] float jumpForce = 10;
    [SerializeField] float gravityModifier = 1;

    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private bool isGrounded = true;
    private bool hasDoubleJumped = false;

    // TODO: Not yet used
    public event Action OnPlayerJump;
    public event Action OnPlayerDeath;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: Remove gameover check
        if (Input.GetButtonDown(jumpButton) && !gameOver)
        {
            if (isGrounded)
            {
                FirstJump();
            }
            else if (!isGrounded && !hasDoubleJumped)
            {
                SecondJump();
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            hasDoubleJumped = false;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            // TODO: Use ondeath event here
            print("Game Over");
            gameOver = true;

            explosionParticle.Play();

            explosionParticle.gameObject.GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }
    }

    // Allows the player to jump upwards
    void FirstJump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound, jumpVolume);
    }

    // Allows the player to jumps upwards a second time
    void SecondJump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        hasDoubleJumped = true;
        // TODO: Change double jump animation
        playerAnim.SetTrigger("Double_Jump_trig");
        playerAudio.PlayOneShot(jumpSound, jumpVolume);
    }
}
