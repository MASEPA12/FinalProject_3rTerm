using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* script that controlls ...
 */

public class GameManager : MonoBehaviour
{
    //CONSTANTS
    private const int MAX_RETRIES = 3; // number of retry before game_over
    private const int INITIAL_HUNGER = 25;
    private const int INITIAL_LIVES = 5;

    //Hunger gauge variables
    public int hunger = INITIAL_HUNGER; //INITIAL VALOR TO FACILITE THE PLAYER 
    public Slider foodCounterSlider;

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

    //random power ups in a random place of an especific zone
    public BoxCollider powerUpZone1;
    public GameObject[] powerUpArray;

    //UI
    public Image[] hearts;

    private void Start()
    {
        //Reset values
        maxLives = INITIAL_LIVES;
        lives = maxLives;

        audioSource = GetComponent<AudioSource>();

        foodCounterSlider.interactable = false; //we lock the interactable option of the food counter slider
        playerMovementScript = FindObjectOfType<PlayerMovement>();
    }


    private void UpdateFoodCounter()
    {
        foodCounterSlider.value = hunger;
        Debug.Log($"{hunger}");
    }


    public IEnumerator LooseFoodTimer()
    {
        while (!isGameOver || isWin) //Player has points
        {
            if (hunger > 0)
            { //Player still has points
                hunger--;
                UpdateFoodCounter();
            }
            else { //Check if the player is Hungry
                UpdateLife(-1); //Lose Life
            }
            yield return new WaitForSeconds(5); //every 5 seconds, looses a point (the player is hungry) ***WHEN POINTS = 0, GAME OVER
        }
    }


    public void DestroyRecollectable(Collider other1,string recollectableName,int pointsToSum)
    {
        if (other1.CompareTag(recollectableName))
        {
            Destroy(other1.gameObject); //destroy bread prefab
            score = score + pointsToSum; //update food score

            //play animation de quan menja 
            // particles play
                
            UpdateFoodCounter();
        }
    }

    
    public IEnumerator LocalScaleTransformer(int secondsToWait)
    {
        playerMovementScript.Scale(2);
        appleRedIsOn = true;

        counterSliderPanel.SetActive(true);

        //play particles
        //play power up red apple sound
        //set active slider que mostra es temps que queda (posar value start corroutine que cada 1 segon en suma 1)

        time = secondsToWait;

        StartCoroutine("Counter"); // posam enmarxa es contador enrrere

        yield return new WaitForSeconds(secondsToWait);


        Debug.Log("aaaaa");

        appleRedIsOn = false;
    }

    private IEnumerator Counter()
    {   //it displays the seconds 

        timeCounterPoweUpSlider.maxValue = time;//posam que es valor màxim de slider sigui es temps que ha d'esperar

        while (time > 0)
        {
            time -= 0.5f;
            timeCounterPoweUpSlider.value = time;
            if(time == 0)
            {
                counterSliderPanel.SetActive(false);
                playerMovementScript.Scale(1.5f);

            }
            yield return new WaitForSeconds(0.5f); //sait 30 seconds to low the number
        }

    }

    public void IsGameOver() //funcition to revise if it's game over
    {

        isGameOver = true;
        //gameOverPanel.SetActive(true);

        //audiosource.audiclip = gameOverSound;
        //audiosource.Play()
        Time.timeScale = 0;
        Debug.Log("YOUR HAVE LOST");

    }

    public void IsHasWin() {
        isWin = true;
        //winPanel.SetActive(true);

        //audiosource.audiclip = gameOverSound;
        //audiosource.Play()
        //Time.timeScale = 0;
        Debug.Log("You Won");
        //Return level menu
    }

    public void UpdateLife(int num) { //
        if (lives > 0 && lives <= maxLives) //5 has to be a variable MAX_lifes
        {
            lives += num;
            GetLife(lives);
        }

        if (lives <= 0){
            IsGameOver();
        }
        
        Debug.Log($" Lifepoints: {lives}");
    }

    public void GetLife(int num) {
        hearts[num].gameObject.SetActive(false);
    }

}
