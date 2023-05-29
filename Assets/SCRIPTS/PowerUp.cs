using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* this script determine the power up mecanic and it's UI */

public class PowerUp : MonoBehaviour
{
    //random power ups in a random place of an especific zone
    public BoxCollider powerUpZone1;
    public GameObject[] powerUpArray;

    //scripts conections
    GameManager gameManagerScript;
    public PlayerMovement playerMovementScript;

    //counter power ups
    public float time;
    public Slider timeCounterPoweUpSlider;
    public GameObject counterSliderPanel;

    //power up bools
    public bool appleRedIsOn;
    public bool isBig;
    public bool isNormalScale;
    public bool isGameOver = false;

    //power up apple green variables
    public bool appleGreenIsOn = false;
    public GameObject sliderPanelGreen;

    // Start is called before the first frame update
    void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>();
        playerMovementScript = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Counter(Slider slider, GameObject panelOfTheSlider, int numOfPowerUp)
    {   //it displays the seconds

        slider.maxValue = time;//posam que es valor màxim de slider sigui es temps que ha d'esperar

        while (time > 0)
        {
            time -= 0.5f;
            slider.value = time;

            //as soon as the time is over, restablish the values
            if (time == 0)
            {
                panelOfTheSlider.SetActive(false);
                if (numOfPowerUp == 1)
                {
                    playerMovementScript.Scale(1.5f);
                }
                if (numOfPowerUp == 2)
                {
                    playerMovementScript.walkingForce = 5f; //restablish the speed
                }

            }
            yield return new WaitForSeconds(0.5f); //wait 30 seconds to low the number
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

        StartCoroutine(Counter(timeCounterPoweUpSlider, counterSliderPanel, 1));

        yield return new WaitForSeconds(secondsToWait);

        appleRedIsOn = false;
    }

    public IEnumerator SpeedPowerUp(float speed, float durationOfPowerUp)
    {
        playerMovementScript.ChangeSpeed(speed); //double up the speed
        appleGreenIsOn = true;
        sliderPanelGreen.SetActive(true);

        time = durationOfPowerUp;
        StartCoroutine(Counter(timeCounterPoweUpSlider, sliderPanelGreen, 2)); //--> ja està dins s'escript de power up (modificada!!!)

        yield return new WaitForSeconds(durationOfPowerUp);
 
        appleGreenIsOn = false;
    }
}
