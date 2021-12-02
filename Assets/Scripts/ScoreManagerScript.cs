using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class ScoreManagerScript : MonoBehaviour
{
    int BestCoins;

    public static ScoreManagerScript instance;
    
    public TextMeshProUGUI CoinText;

    public TextMeshProUGUI endTextCoins;

    public int totalCoins;

    int score;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovementScript.canMove = true;
        instance = this;
        score = 0;
    }

    public void ChangeScore(int coinValue) {
        score += coinValue;
        CoinText.text = score.ToString();
    }

    public void LevelEnded() {
        int numVal = int.Parse(CoinText.text);
        endTextCoins.text = "Coins collected: " + score.ToString() + "/" + totalCoins.ToString();
        if (PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + " coins collected:")) {
            BestCoins = PlayerPrefs.GetInt(SceneManager.GetActiveScene().name + " coins collected:");
            if (totalCoins > BestCoins) {
                PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + " coins collected:", numVal);
            }
        }
        else {
            PlayerPrefs.SetInt(SceneManager.GetActiveScene().name + " coins collected:", numVal);
        }
        PlayerPrefs.Save();
    }
}
