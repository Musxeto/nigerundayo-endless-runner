using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed = 2f;
    public float horizontalSpeed = 3f;
    public float rightLimit = 5.5f;
    public float leftLimit = -5.5f;
    public float jumpForce = 5f;

    private bool isGrounded = true;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
    }

    void Update()
    {

        // Move forward constantly
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        // Move left
        if ((Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed) && transform.position.x > leftLimit)
        {
            transform.Translate(Vector3.left * Time.deltaTime * horizontalSpeed);
        }

        // Move right
        if ((Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed) && transform.position.x < rightLimit)
        {
            transform.Translate(Vector3.right * Time.deltaTime * horizontalSpeed);
        }

        // Jump (only if grounded)
        if ((Keyboard.current.wKey.wasPressedThisFrame || Keyboard.current.upArrowKey.wasPressedThisFrame) && isGrounded)
        {
            Debug.Log("Jumping...");
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name); // Check if it's detecting ground

        if (collision.gameObject.CompareTag("Ground") || rb.position.y < 1.5f)
        {
            Debug.Log("Landed on Ground");
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Left the Ground");
            isGrounded = false;
        }
    }
}
