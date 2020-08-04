using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public variables
    [Header("Particles")]
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;

    [Header("Sounds")]
    public AudioClip jumpSound;
    public float jumpVolume = 1.0f;
    public AudioClip crashSound;
    public float crashVolume = 1.0f;

    // Serialized variables
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

    // Is the player running
    public bool Running { get; private set; } = false;

    // Is the player dead
    public bool Dead { get; private set; } = false;

    // Private variables
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private GameManager gm;
    private Vector3 startingPosition = Vector3.zero;
    private Vector3 deadPosOffset = new Vector3(0, 0, 10);
    private bool isGrounded = true;
    private bool hasDoubleJumped = false;

    // Events
    public event Action OnPlayerFinishWalkIn;
    public event Action OnPlayerJump;
    public event Action OnPlayerStartDash;
    public event Action OnPlayerEndDash;
    public event Action OnPlayerDeath;
    public event Action OnPlayerRevive;

    // Caches required components and sets gravity
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        Physics.gravity *= gravityModifier;

        // Deactive dirt particles
        dirtParticle.gameObject.SetActive(false);

        gm = FindObjectOfType<GameManager>();
        gm.OnGameStart += StartRun;

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

    #region Player animation methods
    /// <summary>
    /// Sets the parameters for the walking animation.
    /// </summary>
    void StartWalkAnim()
    {
        playerAnim.SetBool("Static_b", false);
        playerAnim.SetFloat("Speed_f", 0.5f);
    }

    /// <summary>
    /// Sets the parameters for the crossed arms idle animation.
    /// </summary>
    void StartCrossedArmIdleAnim()
    {
        playerAnim.SetInteger("Animation_int", 1);
        playerAnim.SetFloat("Speed_f", 0);
    }

    /// <summary>
    /// Sets the parameters for the run-in-place animation.
    /// </summary>
    void StartStaticRunAnim()
    {
        playerAnim.SetInteger("Animation_int", 0);
        playerAnim.SetBool("Static_b", true);
        playerAnim.SetFloat("Speed_f", BaseMultiplier);
    }
    #endregion

    /// <summary>
    /// Makes the player walk to the starting position. Assumes the player is to the left of the starting x position.
    /// </summary>
    /// <returns></returns>
    IEnumerator WalkIn()
    {
        StartWalkAnim();

        // Wait until the walking animation brings the player to the starting x position
        while (transform.position.x < startingPosition.x)
        {
            yield return null;
        }

        StartCrossedArmIdleAnim();

        // Constrain x & z movement and all rotation
        playerRb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        OnPlayerFinishWalkIn?.Invoke();
    }

    /// <summary>
    /// Starts the player run animation.
    /// </summary>
    void StartRun()
    {
        if (!Running && !Dead)
        {
            StartStaticRunAnim();

            // Activate dirt particles after 1 second
            StartCoroutine(StartDirtParticles(1f));

            // Set running bool to true
            Running = true;

            // Turn off disabled input
            InputDisabled = false;
        }
    }

    /// <summary>
    /// Ends the player's run.
    /// </summary>
    void EndRun()
    {
        if (Running)
        {
            // Disable input
            InputDisabled = true;

            // Set running bool to false
            Running = false;

            // Stop dirt particles
            dirtParticle.Stop();

            StartCrossedArmIdleAnim();
        }
    }

    /// <summary>
    /// Activates the dirt particle effect after a delay.
    /// </summary>
    /// <param name="delay">The amount of time in seconds to wait.</param>
    /// <returns></returns>
    IEnumerator StartDirtParticles(float delay)
    {
        yield return new WaitForSeconds(delay);
        dirtParticle.gameObject.SetActive(true);
    }

    #region Player action methods
    /// <summary>
    /// Makes the player to jump upwards.
    /// </summary>
    void FirstJump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound, jumpVolume);
        OnPlayerJump?.Invoke();
    }

    /// <summary>
    /// Makes the player jumps a second time while in the air.
    /// </summary>
    void SecondJump()
    {
        playerRb.AddForce(Vector3.up * doubleJumpForce, ForceMode.Impulse);
        hasDoubleJumped = true;
        playerAnim.SetTrigger("Double_Jump_trig");
        playerAudio.PlayOneShot(jumpSound, jumpVolume);
        OnPlayerJump?.Invoke();
    }

    /// <summary>
    /// Makes the player speed up.
    /// </summary>
    void StartDash()
    {
        OnPlayerStartDash?.Invoke();
        playerAnim.SetFloat("Speed_f", DashMultiplier);
    }

    /// <summary>
    /// Returns the player to normal speed.
    /// </summary>
    void EndDash()
    {
        OnPlayerEndDash?.Invoke();
        playerAnim.SetFloat("Speed_f", BaseMultiplier);
    }

    /// <summary>
    /// Sets the player to the grounded state.
    /// </summary>
    void LandOnGround()
    {
        isGrounded = true;
        hasDoubleJumped = false;
        dirtParticle.Play();
    }

    /// <summary>
    /// Kills the player.
    /// </summary>
    void Die()
    {
        EndRun();

        // Set dead bool to true
        Dead = true;

        // Play explosion effect
        explosionParticle.Play();
        explosionParticle.gameObject.GetComponent<AudioSource>().Play();

        // Place player body off screen
        playerRb.isKinematic = true;
        transform.position = startingPosition + deadPosOffset;

        // Inform subscribers
        OnPlayerDeath?.Invoke();
    }

    /// <summary>
    /// Brings the player body to the start position and starts the run.
    /// </summary>
    void Revive()
    {
        if (Dead)
        {
            // The player is alive
            Dead = false;

            // Place player at origin
            transform.position = startingPosition;
            // Set kinematic to false again
            playerRb.isKinematic = false;

            // Begin the run
            StartRun();

            // Inform subscribers player revived
            OnPlayerRevive?.Invoke();
        }
    }
    #endregion
}
