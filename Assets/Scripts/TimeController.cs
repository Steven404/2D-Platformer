using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeController : MonoBehaviour
{
    public static TimeController instance;

    public TextMeshProUGUI timeCounter;

    private TimeSpan timePlaying;
    private bool timerRunning;

    private float elapsedTime;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        timeCounter.text = "Time: 00:00:00";
        timerRunning = false; 
    }

    public void ResumeTimer() {
        timerRunning = true;
    }

    public void PauseTimer() {
        timerRunning = false;
    }

    public void BeginTimer() {
        timerRunning = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }
    
    private IEnumerator UpdateTimer() {
        while (timerRunning) {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = "Time: " + timePlaying.ToString("mm'.'ss'.'ff");
            timeCounter.text = timePlayingStr;
            
            yield return timePlayingStr;
        }
    }
}
