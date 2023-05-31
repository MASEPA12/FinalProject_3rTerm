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
    public int hunger = INITIAL_HUNGER; //INITIAL VALOR TO FACILITE THE PLAYER 
    public Slider foodCounterSlider;
    public float hungerTimer = 2.5f;

    //Life variables
    public int maxLives = INITIAL_LIVES;
    public int lives = 5;
    private int retry = MAX_RETRIES;
    //UI
    public Image[] hearts;

    //script conections
    public PlayerMovement playerMovementScript;

    

    // Start is called before the first frame update
    void Start()
    {
        retry = MAX_RETRIES;
        InitiateValues();
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
            yield return new WaitForSeconds(hungerTimer); //every 5 seconds, looses a point (the player is hungry) ***WHEN POINTS = 0, Loses lifepoints
        }
    }

    //Function that updates life information
    public void UpdateLife(int num)
    { //
        if (lives > 0 && lives <= maxLives) //5 has to be a variable MAX_lifes
        {
            lives += num;
            ShowLife(lives);
        }

        if (lives <= 0)
        {
            CheckRetry();
        }

        Debug.Log($" Lifepoints: {lives}");
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

    public void CheckRetry()
    {
        if (retry > 0)
        {
            retry--;
            Invoke("SetRetry", 2f);
        }
        else
        {
            GameManager.sharedInstance.IsGameOver();
        }
    }

    private void InitiateValues()
    {
        //Reset values
        maxLives = INITIAL_LIVES;
        lives = maxLives;
        hunger = INITIAL_HUNGER;
    }

    public void SetRetry()
    {
        InitiateValues();
        ShowLife(lives);
        playerMovementScript.ResetPosition();
    }
}
