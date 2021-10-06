using UnityEngine;
using TMPro;

public class ScoreManagerScript : MonoBehaviour
{
    public static ScoreManagerScript instance;
    public TextMeshProUGUI text1;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void ChangeScore(int coinValue) {
        score += coinValue;
        text1.text = score.ToString() + "/31";
    }
}
