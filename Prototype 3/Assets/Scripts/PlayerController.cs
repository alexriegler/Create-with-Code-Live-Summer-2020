using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Particles")]
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    [Header("Sounds")]
    public AudioClip jumpSound;
    public float jumpVolume = 1.0f;
    public AudioClip crashSound;
    public float crashVolume = 1.0f;

    [Header("Jump")]
    [SerializeField] float jumpForce = 10;
    [SerializeField] float doubleJumpForce = 5;
    [SerializeField] float gravityModifier = 1;

    [Header("Input")]
    [SerializeField] string jumpButton = "Jump";
    [SerializeField] string dashButton = "Fire3";

    // Determines whether input is accepted or not
    public bool InputDisabled { get; set; } = false;

    // Private variables
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private bool isGrounded = true;
    private bool hasDoubleJumped = false;

    // Events
    public event Action OnPlayerJump;
    public event Action OnPlayerDash;
    public event Action OnPlayerDeath;

    // Caches required components and sets gravity
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;
    }

    // Receives player input
    void Update()
    {
        if (!InputDisabled)
        {
            // Jump input
            if (Input.GetButtonDown(jumpButton))
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

            // Dash input
            if (Input.GetButton(dashButton))
            {
                Dash();
            }
        }
    }

    // Checks player collisions with other objects
    void OnCollisionEnter(Collision collision)
    {
        // Ground collision
        if (collision.gameObject.CompareTag("Ground"))
        {
            LandOnGround();
        }
        // Obstacle collision
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            Die();
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
        OnPlayerJump?.Invoke();
    }

    // Allows the player to jumps upwards a second time
    void SecondJump()
    {
        playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
        hasDoubleJumped = true;
        playerAnim.SetTrigger("Double_Jump_trig");
        playerAudio.PlayOneShot(jumpSound, jumpVolume);
        OnPlayerJump?.Invoke();
    }

    // Causes the player to speed up
    void Dash()
    {
        OnPlayerDash?.Invoke();
        //playerAnim.
    }

    // Sets the player to the grounded state
    void LandOnGround()
    {
        isGrounded = true;
        hasDoubleJumped = false;
        dirtParticle.Play();
    }

    // Kills the player
    void Die()
    {
        InputDisabled = true;
        explosionParticle.Play();
        explosionParticle.gameObject.GetComponent<AudioSource>().Play();
        OnPlayerDeath?.Invoke();
        Destroy(gameObject);
    }
}
