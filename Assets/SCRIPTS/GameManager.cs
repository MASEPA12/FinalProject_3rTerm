using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{

    public static GameManager sharedInstance;

    //Score variables
    public int score = 0; //INITIAL VALOR TO FACILITE THE PLAYER 
    public TextMeshProUGUI scoreText;


    public bool isGameOver = false;
    public bool isWin = false;

    //Scene played
    Scene currentScene;
    
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        score = 0;
        isGameOver = false;
        isWin = false;
        currentScene = SceneManager.GetActiveScene();
        MusicManager.sharedInstance.LevelMusic(currentScene.buildIndex);
    }

    //Function that manages lose condition
    public void IsGameOver() //funcition to revise if it's game over
    {
        isGameOver = true;
        MusicManager.sharedInstance.LoseSound();
        SceneManager.LoadScene(5, LoadSceneMode.Additive);
    }

    //Function that manages win condition
    public void IsHasWin() {
        isWin = true;

        if (DataPersistence.sharedInstance.completedLevels <= currentScene.buildIndex) {
            DataPersistence.sharedInstance.completedLevels = currentScene.buildIndex;
        }

        //Save Progress
        PlayerPrefs.SetInt("LEVELS",DataPersistence.sharedInstance.completedLevels);
        

        MusicManager.sharedInstance.WinSound();
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
    }

    //Check if the game is already over
    public bool IsFinished() {
        return isWin || isGameOver;
    }

    //Function that updates score information
    public void UpdateScore(int points) {
        score += points;
        scoreText.text = $"{score}";
    }

}
