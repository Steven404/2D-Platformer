using UnityEngine;
using TMPro;

public class ScoreManagerScript : MonoBehaviour
{
    public static ScoreManagerScript instance;
    
    public TextMeshProUGUI text1;

    public TextMeshProUGUI endTextCoins;

    public int totalCoins;

    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        PlayerMovementScript.canMove = true;
        instance = this;
    }

    public void ChangeScore(int coinValue) {
        score += coinValue;
        text1.text = score.ToString() + "/" + totalCoins.ToString();
    }

    public void LevelEnded() {
        endTextCoins.text = "Coins collected: " + score.ToString() + "/" + totalCoins.ToString();
    }
}
