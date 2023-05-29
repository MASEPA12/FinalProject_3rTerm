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

    public void SaveUsernameWithPlayerPrefs() {
        PlayerPrefs.SetString("Username", DataPersistence.sharedInstance.username);
    }

    public void SaveWithPlayerPrefs()
    {
        PlayerPrefs.SetString("USERNAME", DataPersistence.sharedInstance.username);
        PlayerPrefs.SetInt("LEVELS", DataPersistence.sharedInstance.completedLevels);
        PlayerPrefs.SetFloat("GENERALVOL", MusicManager.sharedInstance.backgroundVol);
        PlayerPrefs.SetFloat("EFFECTVOL", MusicManager.sharedInstance.effectsVol);
    }

    public void getUsername() {
        existingUsername = PlayerPrefs.GetString("USERNAME");
        if (existingUsername != "") {
            inputField.placeholder.GetComponent<TextMeshProUGUI>().text = existingUsername;
        }
    }
}
