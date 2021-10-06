using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScript : MonoBehaviour
{
    public float timeStart;
    public TextMeshProUGUI timer;
    private bool timerRunning;

    // Start is called before the first frame update
    void Start()
    {
        timer.text = "Time: " + timeStart.ToString("mm':'ss'.'ff");
    }

    // Update is called once per frame
    void Update()
    {
        timeStart += Time.deltaTime;
        timer.text = "Time: " + timeStart.ToString("mm':'ss'.'ff");
    }
}
