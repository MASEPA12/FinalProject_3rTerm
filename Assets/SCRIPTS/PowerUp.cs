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

    private IEnumerator Counter()
    {   //it displays the seconds 

        timeCounterPoweUpSlider.maxValue = time;//posam que es valor màxim de slider sigui es temps que ha d'esperar

        while (time > 0)
        {
            time -= 0.5f;
            timeCounterPoweUpSlider.value = time;
            if (time == 0)
            {
                counterSliderPanel.SetActive(false);
                playerMovementScript.Scale(1.5f);

            }
            yield return new WaitForSeconds(0.5f); //sait 30 seconds to low the number
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
}
