using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void Play() {
        SceneManager.LoadScene("Level 1");
    }

    // Start is called before the first frame update
    public void Exit() {
        Application.Quit();
    }
}