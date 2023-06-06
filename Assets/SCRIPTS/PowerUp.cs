using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/* this script determine the power up mecanic and it's UI */

public class PowerUp : MonoBehaviour
{
    //scripts conections
    public PlayerMovement playerMovementScript;

    //counter power ups
    private float time;
    [SerializeField] private Slider timeCounterPoweUpSlider;
    [SerializeField] public GameObject counterSliderPanel;

    //power up bools
    public bool isBig = false; //Powerup that makes big the player

    //power up apple green variables
    public bool isFast = false; //Powerup that speed up the player
    [SerializeField] private GameObject sliderPanelGreen;

    void Start()
    {
        playerMovementScript = FindObjectOfType<PlayerMovement>();
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
                    playerMovementScript.SetInitialSpeed(); //restablish the speed
                }

            }
            yield return new WaitForSeconds(0.5f); //wait 30 seconds to low the number
        }

    }

    //Coroutine that manages ScaleTransformer power up
    public IEnumerator LocalScaleTransformer(int secondsToWait)
    {
        playerMovementScript.Scale(2);
        MusicManager.sharedInstance.GiantSound();

        isBig = true;
        counterSliderPanel.SetActive(true);

        //play particles
        //play power up red apple sound

        time = secondsToWait;

        StartCoroutine(Counter(timeCounterPoweUpSlider, counterSliderPanel, 1));

        yield return new WaitForSeconds(secondsToWait);

        isBig = false;
    }

    //Coroutine that manages SpeedBoost power up
    public IEnumerator SpeedPowerUp(float speed, float durationOfPowerUp)
    {
        playerMovementScript.ChangeSpeed(speed); //double up the speed
        MusicManager.sharedInstance.FastSound();

        isFast = true;
        sliderPanelGreen.SetActive(true);

        time = durationOfPowerUp;
        StartCoroutine(Counter(timeCounterPoweUpSlider, sliderPanelGreen, 2)); //--> ja està dins s'escript de power up (modificada!!!)

        yield return new WaitForSeconds(durationOfPowerUp);
 
        isFast = false;
    }
}
