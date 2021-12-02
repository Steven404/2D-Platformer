using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {

    [SerializeField] Image SoundOnIcon;
    [SerializeField] Image SoundOffIcon;
    private bool muted;

    // Start is called before the first frame update
    void Start() {
        if (!PlayerPrefs.HasKey("muted")) {
            PlayerPrefs.SetInt("muted", 0);
            Load();
        } else Load();
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    private void UpdateButtonIcon() {
        if (muted == false) {
            SoundOnIcon.enabled = true;
            SoundOffIcon.enabled = false;
        } else {
            SoundOnIcon.enabled = false;
            SoundOffIcon.enabled = true;
        }
    }

    public void OnButtonPress() {
        if (muted == true) {
            muted = false;
            AudioListener.pause = false;
        } else {
            muted = true;
            AudioListener.pause = true;
        }
        Save();
        UpdateButtonIcon();
    }

    private void Load() {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void Save() {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }
}
