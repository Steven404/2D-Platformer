using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighscoreGetter : MonoBehaviour
{
    public TextMeshProUGUI level1BestScore;
    public TextMeshProUGUI level2BestScore;
    public TextMeshProUGUI level3BestScore;
    public TextMeshProUGUI level4BestScore;
    public TextMeshProUGUI level5BestScore;
    public GameObject endText;

    TimeSpan btimeL1;
    TimeSpan btimeL2;
    TimeSpan btimeL3;
    TimeSpan btimeL4;
    TimeSpan btimeL5;

    int bCoinsL1;
    int bCoinsL2;
    int bCoinsL3;
    int bCoinsL4;
    int bCoinsL5;

    // Start is called before the first frame update
    void Start()
    {
        btimeL1 = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level 1 best time:"));
        btimeL2 = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level 2 best time:"));
        btimeL3 = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level 3 best time:"));
        btimeL4 = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level 4 best time:"));
        btimeL5 = TimeSpan.FromSeconds(PlayerPrefs.GetFloat("Level 5 best time:"));
        bCoinsL1 = PlayerPrefs.GetInt("Level 1 coins collected:");
        bCoinsL2 = PlayerPrefs.GetInt("Level 2 coins collected:");
        bCoinsL3 = PlayerPrefs.GetInt("Level 3 coins collected:");
        bCoinsL4 = PlayerPrefs.GetInt("Level 4 coins collected:");
        bCoinsL5 = PlayerPrefs.GetInt("Level 5 coins collected:");
        if (PlayerPrefs.HasKey("Level 1 best time:")) {
            if (PlayerPrefs.GetFloat("Level 1 best time:") !=0 && PlayerPrefs.GetFloat("Level 1 best time:") < 9000) {
                level1BestScore.text = "Level 1\nBest time: " + btimeL1.ToString("mm':'ss':'ff") + "\nMost coins Collected: " + bCoinsL1 + "/31";
            }
        }
        if (PlayerPrefs.HasKey("Level 2 best time:")) {
            if (PlayerPrefs.GetFloat("Level 2 best time:") != 0 && PlayerPrefs.GetFloat("Level 2 best time:") < 9000) {
                level2BestScore.text = "Level 2\nBest time: " + btimeL2.ToString("mm':'ss':'ff") + "\nMost coins Collected: " + bCoinsL2 + "/102";
            }
        }
        if (PlayerPrefs.HasKey("Level 3 best time:")) {
            if (PlayerPrefs.GetFloat("Level 3 best time:") != 0 && PlayerPrefs.GetFloat("Level 3 best time:") < 9000) {
                level3BestScore.text = "Level 3\nBest time: " + btimeL3.ToString("mm':'ss':'ff") + "\nMost coins Collected: " + bCoinsL3 + "/109";
            }
        }
        if (PlayerPrefs.HasKey("Level 4 best time:")) {
            if (PlayerPrefs.GetFloat("Level 4 best time:") != 0 && PlayerPrefs.GetFloat("Level 4 best time:") < 9000) {
                level4BestScore.text = "Level 4\nBest time: " + btimeL4.ToString("mm':'ss':'ff") + "\nMost coins Collected: " + bCoinsL4 + "/133";
            }
        }
        if (PlayerPrefs.HasKey("Level 5 best time:")) {
            if (PlayerPrefs.GetFloat("Level 5 best time:") != 0 && PlayerPrefs.GetFloat("Level 5 best time:") < 9000) {
                level5BestScore.text = "Level 5\nBest time: " + btimeL5.ToString("mm':'ss':'ff") + "\nMost coins Collected: " + bCoinsL5 + "/133";
            }
        }
    }
}
