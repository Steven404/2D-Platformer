using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform RespawnPoint;

    private void OnCollisionEnter2D(Collision2D collision) {
        
        if (collision.gameObject.CompareTag("Lava")) {
            LevelManagerScript.instance.Restart();
        }
    }
}
