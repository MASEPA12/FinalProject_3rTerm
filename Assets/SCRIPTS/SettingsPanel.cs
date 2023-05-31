using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPanel : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;

    private void Start()
    {
        GameIsPaused = false;
        Hide();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel")) {
            if (GameIsPaused)
            {
                Hide();
                GameIsPaused = false;
            }
            else {
                Show();
                GameIsPaused = true;
            }
        }    
    }

    public void GoToScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void RetryScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Hide() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;    
    }

    public void Show() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SoundVol(float volume) {
        MusicManager.sharedInstance.changeVolumen(volume);
    }

    public void EffectVol(float volume)
    {
        MusicManager.sharedInstance.changeEffectVolumen(volume);
    }

}
