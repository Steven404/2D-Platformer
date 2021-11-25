using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public int coinValue = 1;

    public AudioClip coinSound;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Player")){
            ScoreManagerScript.instance.ChangeScore(coinValue);
            AudioSource.PlayClipAtPoint(coinSound,transform.position);
        }
    }
}
