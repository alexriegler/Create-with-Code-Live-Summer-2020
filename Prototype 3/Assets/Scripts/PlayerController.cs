using System;
using System.Collections;
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

    // Public properties
    // Determines whether input is accepted or not
    public bool InputDisabled { get; set; } = true;

    // Multipliers for controlling movement speed
    public float BaseMultiplier { get; private set; } = 1.0f;
    public float DashMultiplier { get; private set; } = 1.5f;

    // Private variables
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private Vector3 startingPosition = Vector3.zero;
    private bool isGrounded = true;
    private bool hasDoubleJumped = false;

    // Events
    public event Action OnPlayerFinishWalkIn;
    public event Action OnPlayerJump;
    public event Action OnPlayerStartDash;
    public event Action OnPlayerEndDash;
    public event Action OnPlayerDeath;

    // Caches required components and sets gravity
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;

        StartCoroutine(WalkIn());
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
            if (Input.GetButtonDown(dashButton))
            {
                StartDash();
            }
            if (Input.GetButtonUp(dashButton))
            {
                EndDash();
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

    // Moves the player to the starting position
    IEnumerator WalkIn()
    {
        // Set conditions for walking animation
        playerAnim.SetBool("Static_b", false);
        playerAnim.SetFloat("Speed_f", 0.5f);

        while (transform.position.x < startingPosition.x)
        {
            print(transform.position);
            yield return null;
        }

        // Set conditions for idle animation
        playerAnim.SetInteger("Animation_int", 1);
        playerAnim.SetFloat("Speed_f", 0);

        // Constrain x & z movement and all rotation
        playerRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        OnPlayerFinishWalkIn?.Invoke();
    }

    // Makes the player run
    void Run()
    {
        // Set conditions for run-in-place animation
        playerAnim.SetBool("Static_b", true);
        playerAnim.SetFloat("Speed_f", BaseMultiplier);
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
    void StartDash()
    {
        OnPlayerStartDash?.Invoke();
        playerAnim.SetFloat("Speed_f", DashMultiplier);
    }

    // Causes the player to return to normal speed
    void EndDash()
    {
        OnPlayerEndDash?.Invoke();
        playerAnim.SetFloat("Speed_f", BaseMultiplier);
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
