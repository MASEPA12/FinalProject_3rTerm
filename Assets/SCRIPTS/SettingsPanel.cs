using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsPanel : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") || Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("No hace nada");
            Show();
        }    
    }

    public void GoToScene(int sceneIndex) {
        SceneManager.LoadScene(sceneIndex);
    }

    public void RetryScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Hide() {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        
    }

    public void Show() {
        gameObject.SetActive(true);
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
