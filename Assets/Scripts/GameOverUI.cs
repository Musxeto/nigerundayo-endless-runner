using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI coinText;

    void Start()
    {
        int finalScore = PlayerPrefs.GetInt("FinalScore", 0);
        int finalCoins = PlayerPrefs.GetInt("FinalCoins", 0);

        scoreText.text = "Score: " + finalScore;
        coinText.text = "Coins: " + finalCoins;
    }
}
