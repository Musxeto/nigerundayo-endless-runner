using TMPro;
using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public TextMeshProUGUI coinText; // Change from TextMeshPro to TextMeshProUGUI
    [SerializeField] private AudioSource coinFx;
    private int coinCount = 0;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.gameObject.name);

        if (other.CompareTag("Coin"))
        {
            coinCount++;
            if (coinFx != null) coinFx.Play();
            if (coinText != null) coinText.text = "Coins: " + coinCount;
            Destroy(other.gameObject);
        }
    }
        public int GetCoinCount()
        {
            return coinCount;
        }
}
