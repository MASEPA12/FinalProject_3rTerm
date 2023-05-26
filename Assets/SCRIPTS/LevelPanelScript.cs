using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPanelScript : MonoBehaviour
{
    public Image[] arrowArray;
    public Image[] girlIcon;

    //aquests bools les necessitam per fer lock/unlock als nivells, no se exactament a quin script anirien
    public bool hasWonLev1;
    public bool hasWonLev2;
    public bool hasWonLev3;

    //private Button locatorButtonLev1; --> no se si el necessitam, mirar-ho
    public Button locatorButtonLev2;
    public Button locatorButtonLev3;

    private bool arrowsAreShowing1;
    private bool arrowsAreShowing2;
    private bool arrowsAreShowing3;

    private void Start()
    {
        girlIcon[1].gameObject.SetActive(false);
        girlIcon[2].gameObject.SetActive(false);
        girlIcon[3].gameObject.SetActive(false);

        locatorButtonLev2.gameObject.SetActive(false);
        locatorButtonLev3.gameObject.SetActive(false);
    }
    void Update()
    {
        //if the player has not won any level, the default will be the girl icon in the level 1
        if(hasWonLev1 == true && arrowsAreShowing1 == false)
        {
            girlIcon[0].gameObject.SetActive(false); 
            //has to show 4 arrows
            StartCoroutine(showArrows(4));
            locatorButtonLev2.gameObject.SetActive(true); //això ho hauriem de posar sa data persistance 
            
            girlIcon[1].gameObject.SetActive(true);
            arrowsAreShowing1 = true;
        }
        if (hasWonLev2 == true && arrowsAreShowing2 == false)
        {
            girlIcon[1].gameObject.SetActive(false);

            //has to show 4 arrows
            StartCoroutine(showArrows(9));
            locatorButtonLev3.gameObject.SetActive(true);
            
            girlIcon[2].gameObject.SetActive(true);
            arrowsAreShowing2 = true;
        }
        if (hasWonLev3 == true && arrowsAreShowing3 == false)
        {
            girlIcon[2].gameObject.SetActive(false);
            girlIcon[3].gameObject.SetActive(true);

            //has to show all the arrows
            StartCoroutine(showArrows(arrowArray.Length-1));
            arrowsAreShowing3 = true;
        }
    }

    private IEnumerator showArrows(int a)
    {
        for (int i = 0; i <= a; i++)
        {
            Debug.Log("un pic");
            arrowArray[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
