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
        if (Input.GetButtonDown(jumpButton) && isGrounded && !gameOver)
        {
            FirstJump();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            dirtParticle.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            print("Game Over");
            gameOver = true;

            explosionParticle.Play();

            explosionParticle.gameObject.GetComponent<AudioSource>().Play();

            Destroy(gameObject);
        }
    }

    // Jumps the player upwards
    void FirstJump()
    {
        playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
        playerAnim.SetTrigger("Jump_trig");
        dirtParticle.Stop();
        playerAudio.PlayOneShot(jumpSound, jumpVolume);
    }
}
