using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour {
    public GameObject LoadSceen;

    public GameObject MainMenuCanvas;

    public GameObject LevelCanvas;

    public GameObject HighScoresCanvas;

    public GameObject LastWarningCanvas;

    public Slider slider;

    public static LevelLoader instance;

    private void Awake() {
        instance = this;
    }

    public void ShowLevelCanvas() {
        MainMenuCanvas.SetActive(false);
        LevelCanvas.SetActive(true);
    }

    public void ShowHighscoresCavnas() {
        MainMenuCanvas.SetActive(false);
        LastWarningCanvas.SetActive(false);
        HighScoresCanvas.SetActive(true);
    }

    public void ShowMainMenuCanvas() {
        LevelCanvas.SetActive(false);
        HighScoresCanvas.SetActive(false);
        LastWarningCanvas.SetActive(false);
        MainMenuCanvas.SetActive(true);
    }

    public void LoadScene(int sceneIndex) {
        StartCoroutine(LoadAsynchronously(sceneIndex));
    }

    public void LastWarning() {
        LastWarningCanvas.SetActive(true);
        LevelCanvas.SetActive(false);
        HighScoresCanvas.SetActive(false);
        MainMenuCanvas.SetActive(false);
    }

    public void resetHighscores() {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator LoadAsynchronously (int sceneIndex) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        LoadSceen.SetActive(true);

        while (!operation.isDone) {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            slider.value = progress;

            yield return null;
        }
    }
}
