using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 5f;
    public float horizontalSpeed = 3f;
    public float rightLimit = 5.5f;
    public float leftLimit = -5.5f;
    public float jumpForce = 7f;
    public TextMeshProUGUI scoreText;
    private CollisionDetect collisionDetect;
    [SerializeField] private AudioSource jumpFx;
    private bool isGrounded = true;
    private Rigidbody rb;
    private float startZ;
    private int score = 0;
    public Animator animator; 
    private bool isDead = false;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        startZ = transform.position.z;

        if (animator == null)
            animator = GetComponent<Animator>(); 

        collisionDetect = GetComponent<CollisionDetect>();
    }

    void Update()
    {
        if (isDead) return; // ✅ Stops everything if the player is dead

        // Forward movement
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        // Horizontal movement
        if ((Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) && transform.position.x > leftLimit)
        {
            transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed);
        }
        if ((Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) && transform.position.x < rightLimit)
        {
            transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed);
        }
        if (Keyboard.current.escapeKey.isPressed)
        {
            collisionDetect.LoadGameOverScene();
        }

        if ((Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.spaceKey.wasPressedThisFrame) && isGrounded)
        {
            Debug.Log("Jumping...");
            jumpFx.Play();

            if (animator != null)
                animator.Play("Jump"); 

            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z); 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false;
        }

       
        if (!isDead && isGrounded && animator != null)
            animator.Play("Run");

        score = Mathf.FloorToInt(transform.position.z - startZ);
        scoreText.text = "Score: " + score;
        if (score > 100 && score%50 == 0)
            playerSpeed += 0.1f;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !isDead) 
        {
            isGrounded = true;
            if (animator != null)
                animator.Play("Run");
        }
    }


    public int GetScore()
    {
        return score;
    }
    public void SetDeadState()
    {
        isDead = true; 
    }


}
