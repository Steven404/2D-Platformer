using UnityEngine;
using TMPro;

public class ScoreManagerScript : MonoBehaviour
{
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
        endTextCoins.text = "Coins collected: " + score.ToString() + "/" + totalCoins.ToString();
    }
}
