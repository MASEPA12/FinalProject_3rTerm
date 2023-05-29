using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/* script that controlls ...
 */

public class GameManager : MonoBehaviour
{

    public GameManager sharedInstance;

    //CONSTANTS
    private const int MAX_RETRIES = 3; // number of retry before game_over
    private const int INITIAL_HUNGER = 25;
    private const int INITIAL_LIVES = 5;

    //Hunger gauge variables
    public int hunger = INITIAL_HUNGER; //INITIAL VALOR TO FACILITE THE PLAYER 
    public Slider foodCounterSlider;
    public float hungerTimer = 2.5f;

    //Score variables
    public int score = 0; //INITIAL VALOR TO FACILITE THE PLAYER 
    public TextMeshProUGUI scoreText;

    //counter power ups
    public float time;
    public Slider timeCounterPoweUpSlider;
    public GameObject counterSliderPanel;

    //power up bools
    public bool appleRedIsOn;
    public bool isBig;
    public bool isNormalScale;
    public bool isGameOver = false;
    public bool isWin = false;

    //Life variables
    public int maxLives = INITIAL_LIVES;
    public int lives = 5;
    private int retry = MAX_RETRIES;

    //script conections
    public PlayerMovement playerMovementScript;

    //sounds
    public AudioClip gameOverAudioClip; //renou de quan perd
    public AudioClip jumpAudioclip; //renou de quan bota
    public AudioClip shootedAudioclip;//renou de quan li han pegat
    public AudioSource audioSource;

    //particles
    public ParticleSystem jumpingParticles;


    //UI
    public Image[] hearts;

    //Scene played
    Scene currentScene;
    
    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Start()
    {
        InitiateValues();
        score = 0;
        audioSource = GetComponent<AudioSource>();

        foodCounterSlider.interactable = false; //we lock the interactable option of the food counter slider
        playerMovementScript = FindObjectOfType<PlayerMovement>();
        currentScene = SceneManager.GetActiveScene();
    }


    //Function that updates the value of hunger gauge
    private void UpdateFoodCounter()
    {
        foodCounterSlider.value = hunger;
    }


    //Coroutine that manages the hunger Gauge
    public IEnumerator LooseFoodTimer()
    {
        while (!isGameOver || !isWin) //Player has points
        {
            if (hunger > 0)
            { //Player still has points
                UpdateHunger(-1);
            }
            else { //Check if the player is Hungry
                UpdateLife(-1); //Lose Life
            }
            yield return new WaitForSeconds(hungerTimer); //every 5 seconds, looses a point (the player is hungry) ***WHEN POINTS = 0, Loses lifepoints
        }
    }


    public void DestroyRecollectable(Collider other1,string recollectableName,int pointsToSum)
    {
        if (other1.CompareTag(recollectableName))
        {
            Destroy(other1.gameObject); //destroy bread prefab
            //score = score + pointsToSum; //update food score

            //play animation de quan menja 
            // particles play
                
            UpdateFoodCounter();
        }
    }

    //Function that manages lose condition
    public void IsGameOver() //funcition to revise if it's game over
    {

        isGameOver = true;
        //gameOverPanel.SetActive(true);

        //audiosource.audiclip = gameOverSound;
        //audiosource.Play()
        //Time.timeScale = 0;
        SceneManager.LoadScene(5, LoadSceneMode.Additive);
        Debug.Log("YOUR HAVE LOST");

    }

    //Function that manages win condition
    public void IsHasWin() {
        isWin = true;

        if (DataPersistence.sharedInstance.completedLevels <= currentScene.buildIndex) {
            DataPersistence.sharedInstance.completedLevels = currentScene.buildIndex;
        }

        //audiosource.audiclip = gameOverSound;
        //audiosource.Play()
        SceneManager.LoadScene(4, LoadSceneMode.Additive);
        Debug.Log("You Won");
        //Return level menu
    }

    //Function that updates life information
    public void UpdateLife(int num) { //
        if (lives > 0 && lives <= maxLives) //5 has to be a variable MAX_lifes
        {
            lives += num;
            ShowLife(lives);    
        }

        if (lives <= 0){
            DoRetry();
        }
        
        Debug.Log($" Lifepoints: {lives}");
    }

    //Function that updates life UI
    public void ShowLife(int num) {
        for(int i = 0; i < maxLives; i++)
        {
            if (i > num - 1)
            {
                hearts[i].gameObject.SetActive(false);
            }
            else {
                hearts[i].gameObject.SetActive(true);
            }

        }
    }

    //Function that updates score information
    public void UpdateScore(int points) {
        score += points;
        scoreText.text = $"SCORE\n{score}";
    }

    //Function that update hunger information
    public void UpdateHunger(int hungerPoints) {
        hunger += hungerPoints;
        foodCounterSlider.value = hunger;
    }

    private void DoRetry() {
        if (retry > 0)
        {
            retry--;
            InitiateValues();
            ShowLife(lives);
            playerMovementScript.ResetPosition();
        }
        else {
            IsGameOver();
        }
    }

    private void InitiateValues()
    {
        //Reset values
        maxLives = INITIAL_LIVES;
        lives = maxLives;
        hunger = INITIAL_HUNGER;
    }
}
