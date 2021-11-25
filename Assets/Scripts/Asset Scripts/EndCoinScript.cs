using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndCoinScript : MonoBehaviour
{

    public AudioClip EndCoinSound;

    public void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            TimeController.instance.EndTimer();
            AudioSource.PlayClipAtPoint(EndCoinSound, transform.position);
        }
    }

}
