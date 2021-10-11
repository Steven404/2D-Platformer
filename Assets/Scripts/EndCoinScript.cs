using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndCoinScript : MonoBehaviour
{
    public GameObject EndPanel;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            TimeController.instance.EndTimer();
            EndPanel.SetActive(true);
        }
    }

}
