using UnityEngine;
using UnityEngine.SceneManagement; // Import Scene Management

public class CollisionDetect : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private CoinCollector coinCollector;
    public GameObject playerAnim;
    [SerializeField] AudioSource collisionFX;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject fadeOut;

    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>(); 
        coinCollector = GetComponent<CoinCollector>();
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
                fadeOut.SetActive(true);

                // Save Score & Coins
                PlayerPrefs.SetInt("FinalScore", playerMovement.GetScore()); 
                PlayerPrefs.SetInt("FinalCoins", coinCollector.GetCoinCount()); 
                PlayerPrefs.Save();

                // Load Game Over Scene after delay
                Invoke("LoadGameOverScene", 3f);
            }
        }
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(2); // Make sure Scene 2 is in Build Settings
    }
}
