using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{
    public TimeSpan BestTime;

    public GameObject InGameCanvas;

    public GameObject ExitButton;

    public GameObject ContinueButton;

    public GameObject EndPanel;

    public GameObject BestTimeText;

    public GameObject TimeText;

    public GameObject CoinText;

    public static TimeController instance;

    public TextMeshProUGUI BestTimeTMP;

    public TextMeshProUGUI timeCounter;

    public TextMeshProUGUI timeNeeded;


    private TimeSpan timePlaying;
    private bool timerRunning;

    private float elapsedTime;

    private void Awake() {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        bool highScoreWritten = PlayerPrefs.HasKey(SceneManager.GetActiveScene().name + " best time:");
        float btime = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + " best time:");
        Debug.Log(btime);
        if (btime < 7 || highScoreWritten == false) {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + " best time:", 9000);
        }
        timeCounter.text = "Time: 00:00.00";
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

    public void EndTimer() {
        HighScore();
        PlayerMovementScript.canMove = false;
        timerRunning = false;
        StartCoroutine(EndLevel());
    }

    private IEnumerator EndLevel() {
        BestTime = TimeSpan.FromSeconds(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + " best time:"));
        yield return new WaitForSeconds(2);
        InGameCanvas.SetActive(false);
        yield return new WaitForSeconds(1);
        timePlaying = TimeSpan.FromSeconds(elapsedTime);
        EndPanel.SetActive(true);
        yield return new WaitForSeconds(1);
        TimeText.SetActive(true);
        BestTimeText.SetActive(true);
        yield return new WaitForSeconds(1f);
        timeNeeded.text = "Time: " + timePlaying.ToString("mm':'ss'.'ff");
        BestTimeTMP.text = "Best Time: " + BestTime.ToString("mm':'ss'.'ff");
        yield return new WaitForSeconds(1);
        CoinText.SetActive(true);
        yield return new WaitForSeconds(1f);
        ScoreManagerScript.instance.LevelEnded();
        yield return new WaitForSeconds(1);
        ExitButton.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        ContinueButton.SetActive(true);
    }

    public void HighScore() {
        TimeSpan Time = TimeSpan.FromSeconds(elapsedTime);
        BestTime = TimeSpan.FromSeconds(PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name + " best time:"));
        if (Time < BestTime) {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name + " best time:", elapsedTime);
            PlayerPrefs.Save();
        }
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