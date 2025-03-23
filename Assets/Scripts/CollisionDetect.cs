using TMPro;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    private PlayerMovement playerMovement;
    public GameObject playerAnim;
    [SerializeField]  AudioSource collisionFX;
     [SerializeField]  GameObject cam;
     [SerializeField]  GameObject fadeOut;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); 
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle")) 
        {
            Debug.Log("Collision with obstacle! Disabling PlayerMovement.");
            if (playerMovement != null)
            {
                playerMovement.enabled = false; 
                collisionFX.Play();
                playerAnim.GetComponent<Animator>().Play("Stumble Backwards");
                cam.GetComponent<Animator>().Play("CollisionCam");
                // yield return new WaitForSeconds(3);
                fadeOut.SetActive(true);
            }
        }
    }
    
}
