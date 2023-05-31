using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/* 
 * script that controlls ...
 */

public class GameManager : MonoBehaviour
{

    public static GameManager sharedInstance;

        //Score variables
    public int score = 0; //INITIAL VALOR TO FACILITE THE PLAYER 
    public TextMeshProUGUI scoreText;
    

    /*
    //counter power ups
    public float time;
    public Slider timeCounterPoweUpSlider;
    public GameObject counterSliderPanel;
    */

    /*
    //power up bools
    public bool appleRedIsOn;
    public bool isBig;
    public bool isNormalScale;
    */

    public bool isGameOver = false;
    public bool isWin = false;
    private bool isGamePause = false;

    //Scene played
    Scene currentScene;
    
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        //InitiateValues();
        score = 0;
        isGameOver = false;
        isWin = false;
        //foodCounterSlider.interactable = false; //we lock the interactable option of the food counter slider
        currentScene = SceneManager.GetActiveScene();
        MusicManager.sharedInstance.LevelMusic(currentScene.buildIndex);
    }

    //Function that manages lose condition
    public void IsGameOver() //funcition to revise if it's game over
    {
        isGameOver = true;
        MusicManager.sharedInstance.LoseSound();
        SceneManager.LoadScene(5, LoadSceneMode.Additive);
        Debug.Log("YOU HAVE LOST");

    }

    //Function that manages win condition
    public void IsHasWin() {
        isWin = true;

        if (DataPersistence.sharedInstance.completedLevels <= currentScene.buildIndex) {
            DataPersistence.sharedInstance.completedLevels = currentScene.buildIndex;
        }

        MusicManager.sharedInstance.WinSound();
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
        Debug.Log("You Won");
        //Return level menu
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

    private void TogglePauseGame() {
        isGamePause = !isGamePause;
        if (isGamePause)
        {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 0f;
        }
    }
}
