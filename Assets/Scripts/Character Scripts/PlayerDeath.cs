using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform RespawnPoint;
    [SerializeField] private GameObject DeathPanel;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Lava")) {
            Destroy(player);
            DeathPanel.SetActive(true);
        }
    }

    private void Update() {
        if (DeathPanel.active) {
            Debug.Log("sex");
            if (Input.GetButtonDown("Space") || Input.GetButtonDown("Enter")) {
                LevelManagerScript.instance.Restart();
            }
        }
    }
}
