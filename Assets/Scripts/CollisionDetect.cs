using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public GameObject playerAnim;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); // Get reference to PlayerMovement
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle")) 
        {
            Debug.Log("Collision with obstacle! Disabling PlayerMovement.");
            if (playerMovement != null)
            {
                playerMovement.enabled = false; 
                playerAnim.GetComponent<Animator>().Play("Stumble Backwards");
            }
        }
    }
}
