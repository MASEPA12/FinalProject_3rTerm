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

    private void Start()
    {
        foodCounterSlider.interactable = false; //we lock the interactable option of the food counter slider
    }

    private void OnTriggerEnter(Collider other)
    {
       
    }

    //function to update food counter slider
    private void UpdateFoodCounter() 
    {
        points++;
        foodCounterSlider.value = points;
    }
    public void DestroyRecollectable(Collider other1,string recollectableName,int pointsToSum)
    {
        if (other1.CompareTag(recollectableName))
            {
                Destroy(other1); //destroy bread prefab
                points =+ pointsToSum; //update food score
                Debug.Log($"has sumat {points} punts");
                //play animation de quan menja // particles play
            }
    }
    
}
