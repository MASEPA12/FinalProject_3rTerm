using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TMP_InputField inputField;
    private string existingUsername;

    //Function to load desired level to play
    public void PlayScene(int sceneNumber) {
        SceneManager.LoadScene(sceneNumber);
    }

    //Function to return to main menu
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    //Restart current level
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        GetUsername();
    }

    public void SaveUsername() {
        string inputText = inputField.text;

        if (inputText == "")
        {
            DataPersistence.sharedInstance.username = inputField.placeholder.GetComponent<TextMeshProUGUI>().text;
        }
        else {
            DataPersistence.sharedInstance.username = inputText;
        }
    }

    public void SoundVol(float volume)
    {
        MusicManager.sharedInstance.changeVolumen(volume);
    }

    public void EffectVol(float volume)
    {
        MusicManager.sharedInstance.changeEffectVolumen(volume);
    }

    public void SaveUsernameWithPlayerPrefs() {
        PlayerPrefs.SetString("USERNAME", DataPersistence.sharedInstance.username);
    }

    public void SaveWithPlayerPrefs()
    {
        PlayerPrefs.SetString("USERNAME", DataPersistence.sharedInstance.username);
        PlayerPrefs.SetInt("LEVELS", DataPersistence.sharedInstance.completedLevels);
        PlayerPrefs.SetFloat("GENERALVOL", MusicManager.sharedInstance.backgroundVol);
        PlayerPrefs.SetFloat("EFFECTVOL", MusicManager.sharedInstance.effectsVol);
    }

    public void SaveSoundWithPlayerPrefs()
    {
        PlayerPrefs.SetFloat("GENERALVOL", MusicManager.sharedInstance.backgroundVol);
        PlayerPrefs.SetFloat("EFFECTVOL", MusicManager.sharedInstance.effectsVol);
    }

    //Function that load username
    public void GetUsername() {
        existingUsername = PlayerPrefs.GetString("USERNAME");
        if (existingUsername != "") {
            inputField.placeholder.GetComponent<TextMeshProUGUI>().text = existingUsername;
        }
    }

    /*
    //Function that loads de progress
    public void LoadProgres() {
        if (inputField.text == "" && inputField.placeholder.GetComponent<TextMeshProUGUI>().text == PlayerPrefs.GetString("USERNAME")) {
            DataPersistence.sharedInstance.completedLevels = PlayerPrefs.GetInt("LEVELS");
        }
    }
    */

    //Function that Quits the Game
    public void ExitGame() {
        Application.Quit();
    }


}
