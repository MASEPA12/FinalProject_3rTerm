using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    //CONSTANTS
    private const int MAX_RETRIES = 3; // number of retry before game_over
    private const int INITIAL_HUNGER = 25;
    private const int INITIAL_LIVES = 5;

    //Hunger gauge variables
    [SerializeField] private int hunger = INITIAL_HUNGER; //INITIAL VALOR TO FACILITE THE PLAYER 
    [SerializeField] private Slider foodCounterSlider;
     private float hungerTimer = 2.5f;

    //Life variables
    private int maxLives = INITIAL_LIVES;
    public int lives = 5;
    private int retry = MAX_RETRIES;
    [SerializeField] private TextMeshProUGUI retryText;

    //UI
    public Image[] hearts;

    //script conections
    private PlayerMovement playerMovementScript;

    

    // Start is called before the first frame update
    void Start()
    {
        playerMovementScript = FindObjectOfType<PlayerMovement>();
        retry = MAX_RETRIES;
        InitiateValues();

        retryText.text = $"x{retry}";
        foodCounterSlider.interactable = false; //we lock the interactable option of the food counter slider
        StartCoroutine(LooseFoodTimer());
    }

    //Coroutine that manages the hunger Gauge
    public IEnumerator LooseFoodTimer()
    {
        while (!GameManager.sharedInstance.IsFinished()) //While Game hasn't finish
        {
            if (hunger > 0)
            { //Player still has points
                UpdateHunger(-1);

            }
            else
            { //Check if the player is Hungry
                UpdateLife(-1); //Lose Life
                //postProcesingManager.VignetteOn(0.5f, Color.red);
            }
            yield return new WaitForSeconds(hungerTimer); //every 2.5 seconds, looses a point (the player is hungry) ***WHEN POINTS = 0, Loses lifepoints
        }
    }

    //Function that updates life information
    public void UpdateLife(int num)
    { //
        if (lives > 0 && lives <= maxLives) //5 has to be a variable MAX_lifes
        {
            if (!(num > 0 && lives == maxLives)) { //Avoid to restore heart when lives == maxLives
                lives += num;
                ShowLife(lives);
            }
        }

        if (lives <= 0)
        {   
            CheckRetry();
        }

    }

    //Function that updates life UI
    public void ShowLife(int num)
    {
        for (int i = 0; i < maxLives; i++)
        {
            if (i > num - 1)
            {
                hearts[i].gameObject.SetActive(false);
            }
            else
            {
                hearts[i].gameObject.SetActive(true);
            }

        }
    }

    //Function that update hunger information
    public void UpdateHunger(int hungerPoints)
    {
        hunger += hungerPoints;
        foodCounterSlider.value = hunger;
    }

    //Function that check if can retry or if is gameover
    public void CheckRetry()
    {
        if (retry > 0)
        {
            retry--;
            retryText.text = $"x{retry}"; //Update text
            Invoke("SetRetry", 1f);
        }

        if(retry <=0)
        {
            GameManager.sharedInstance.IsGameOver();
        }
    }

    //Function that initiate values
    private void InitiateValues()
    {
        //Reset values
        maxLives = INITIAL_LIVES;
        lives = maxLives;
        hunger = INITIAL_HUNGER;
    }

    //Function that reset position and health values
    public void SetRetry()
    {
        InitiateValues();
        ShowLife(lives);
        playerMovementScript.ResetPosition();
    }
}
