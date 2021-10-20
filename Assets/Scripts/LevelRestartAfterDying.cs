using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelRestartAfterDying : MonoBehaviour
{
    public GameObject panel;

    // Update is called once per frame
    void Update()
    {
        if (panel.activeInHierarchy) {
            if (Input.GetButtonDown("Jump")) {
                LevelManagerScript.instance.Restart();
            }
        }
    }
}
