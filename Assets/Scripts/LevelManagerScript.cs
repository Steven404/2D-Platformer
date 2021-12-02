using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManagerScript : MonoBehaviour
{
    public Transform respawnPoint;

    public static LevelManagerScript instance;

    public GameObject playerPrefab;

    private void Awake() {
        instance = this;
    }

    public void Start() {
        TimeController.instance.BeginTimer();
    }

    public void Respawn() {
        Instantiate(playerPrefab, respawnPoint.position, Quaternion.identity);
    }

    public void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        playerPrefab.SetActive(true);
    }

    public void DisableCanvas(GameObject gameObject) {
        gameObject.SetActive(false);
       
    }

    public void DestroyTutorial(GameObject tutorial) {
        Destroy(tutorial);
        Time.timeScale = 1f;
    }
}
