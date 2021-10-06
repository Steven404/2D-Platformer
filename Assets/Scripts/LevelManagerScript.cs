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
}
