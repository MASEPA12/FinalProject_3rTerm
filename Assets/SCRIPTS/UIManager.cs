using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
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

}
