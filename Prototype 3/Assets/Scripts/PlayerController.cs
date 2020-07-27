using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] string jumpButton = "Jump";
    [SerializeField] float jumpForce = 10;
    
    private Rigidbody playerRb;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
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
