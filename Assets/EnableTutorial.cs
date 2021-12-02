using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableTutorial : MonoBehaviour
{
    public GameObject TutorialPanel;

    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")) TutorialPanel.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.GamePaused = true;
    }
}
