using UnityEngine;
using UnityEngine.UI;

public class CoinCollector : MonoBehaviour
{
    public Text coinText;  // Assign a UI Text object in the Inspector
    private int coinCount = 0;

    void OnTriggerEnter(Collider other)
    {
    Debug.Log("Triggered by: " + other.gameObject.name); // Check what object is triggering

    if (other.CompareTag("Coin"))
    {
        Debug.Log("Coin collected!"); // Check if the tag is being recognized
        Destroy(other.gameObject); 
    }
}

}
