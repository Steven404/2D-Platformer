using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveSystem : MonoBehaviour
{
    float btimeL1;
    float btimeL2;
    float btimeL3;
    float btimeL4;
    float btimeL5;

    [Header("Buttons")]
    [SerializeField] public Button Level2Button;
    [SerializeField] public Button Level3Button;
    [SerializeField] public Button Level4Button;
    [SerializeField] public Button Level5Button;

    // Start is called before the first frame update
    void Start()
    {
        btimeL1 = PlayerPrefs.GetFloat("Level 1 best time:");
        btimeL2 = PlayerPrefs.GetFloat("Level 2 best time:");
        btimeL3 = PlayerPrefs.GetFloat("Level 3 best time:");
        btimeL4 = PlayerPrefs.GetFloat("Level 4 best time:");
        btimeL5 = PlayerPrefs.GetFloat("Level 5 best time:");
        if (btimeL1 != 0 && btimeL1 < 9000) Level2Button.interactable = true;
        if (btimeL2 != 0 && btimeL2 < 9000) Level3Button.interactable = true;
        if (btimeL3 != 0 && btimeL3 < 9000) Level4Button.interactable = true;
        if (btimeL4 != 0 && btimeL4 < 9000) Level5Button.interactable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
