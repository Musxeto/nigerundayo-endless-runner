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
                playerMovement.SendMessage("SetDeadState"); 

                collisionFX.Play();
                playerAnim.GetComponent<Animator>().Play("Stumble Backwards");
                cam.GetComponent<Animator>().Play("CollisionCam");
                fadeOut.SetActive(true);

                PlayerPrefs.SetInt("FinalScore", playerMovement.GetScore());
                PlayerPrefs.SetInt("FinalCoins", coinCollector.GetCoinCount());
                PlayerPrefs.Save();

                Invoke("LoadGameOverScene", 3f);
            }

        }
    }

    public void LoadGameOverScene()
    {
        SceneManager.LoadScene(2); 
    }
}
