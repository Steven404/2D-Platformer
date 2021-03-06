using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
    public List<Transform> points;
    public Transform platform;
    int goalPoint = 0;
    public float speed;

    public static PlatformMove instance;

    private void Awake() {
        instance = this;
    }

    public void FixedUpdate() {
        MoveToNextPoint();
    }

    public float returnSpeed() {
        return speed;
    }

    void MoveToNextPoint() {
        platform.position = Vector2.MoveTowards(platform.position, points[goalPoint].position, speed * Time.deltaTime);
        if (Vector2.Distance(platform.position, points[goalPoint].position) < 0.1f){
            if (goalPoint == points.Count - 1) goalPoint = 0;
            else goalPoint++;
        }
    }

    //code for teleporting platform

    //private bool run

    /*private IEnumerator MoveToNextPoint() {
        while (run) {
            platform.position = Vector2.MoveTowards(platform.position, points[goalPoint].position, speed * Time.deltaTime);
            if (Vector2.Distance(platform.position, points[goalPoint].position) < 0.1f) {
                if (goalPoint == points.Count - 1) goalPoint = 0;
                else goalPoint++;
                yield return new WaitForSeconds(2);
            }
        }

    }*/
}
