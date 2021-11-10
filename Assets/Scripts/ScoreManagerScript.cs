using UnityEngine;
using TMPro;

public class ScoreManagerScript : MonoBehaviour
{
    public static ScoreManagerScript instance;
    
    public TextMeshProUGUI CoinText;

    public TextMeshProUGUI endTextCoins;

    public int totalCoins;

    int lmao = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovementScript.canMove = true;
        instance = this;
    }

    public void ChangeScore(int coinValue) {
        lmao += coinValue;
        CoinText.text = lmao.ToString();
    }

    public void LevelEnded() {
        endTextCoins.text = "Coins collected: " + lmao.ToString() + "/" + totalCoins.ToString();
    }
}
