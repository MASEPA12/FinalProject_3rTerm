using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/* script that controlls ...
 */

public class GameManager : MonoBehaviour
{
    public int points;
    public Slider foodCounterSlider;

    //counter power ups
    public float time;
    public Slider timeCounterPoweUpSlider;
    public GameObject counterSliderPanel;

    //powear up bools
    public bool appleRedIsOn;


    private void Start()
    {
        foodCounterSlider.interactable = false; //we lock the interactable option of the food counter slider
    }

    //function to update food counter slider
    private void UpdateFoodCounter() 
    {
        foodCounterSlider.value = points;
    }

    public void DestroyRecollectable(Collider other1,string recollectableName,int pointsToSum)
    {
        if (other1.CompareTag(recollectableName))
            {
                Destroy(other1.gameObject); //destroy bread prefab
                points = points + pointsToSum; //update food score

                //play animation de quan menja 
                // particles play
                
                UpdateFoodCounter();
            }
    }

    public IEnumerator LocalScaleTransformer(int secondsToWait)
    {

        appleRedIsOn = true;

        counterSliderPanel.SetActive(true);

        //play particles
        //play power up red apple sound
        //set active slider que mostra es temps que queda (posar value start corroutine que cada 1 segon en suma 1)

        time = secondsToWait;

        StartCoroutine("Counter"); // posam enmarxa es contador enrrere

        yield return new WaitForSeconds(secondsToWait);
        transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);

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
            }
            yield return new WaitForSeconds(0.5f); //sait 30 seconds to low the number
        }

    }


}
