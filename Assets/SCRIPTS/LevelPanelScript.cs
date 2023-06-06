using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPanelScript : MonoBehaviour
{
    public Image[] arrowArray;
    public Image[] girlIcon;

    //Button 
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
        if (DataPersistence.sharedInstance.completedLevels>=1 && arrowsAreShowing1 == false)
        {
            girlIcon[0].gameObject.SetActive(false);
            //has to show 4 arrows
            StartCoroutine(showArrows(4));
            locatorButtonLev2.gameObject.SetActive(true); 

            girlIcon[1].gameObject.SetActive(true);
            arrowsAreShowing1 = true;
        }
        if (DataPersistence.sharedInstance.completedLevels >= 2 && arrowsAreShowing2 == false)
        {
            girlIcon[1].gameObject.SetActive(false);

            //has to show 4 arrows
            StartCoroutine(showArrows(9));
            locatorButtonLev3.gameObject.SetActive(true);

            girlIcon[2].gameObject.SetActive(true);
            arrowsAreShowing2 = true;
        }
        if (DataPersistence.sharedInstance.completedLevels >= 3 && arrowsAreShowing3 == false)
        {
            girlIcon[2].gameObject.SetActive(false);
            girlIcon[3].gameObject.SetActive(true);

            //has to show all the arrows
            StartCoroutine(showArrows(arrowArray.Length - 1));
            arrowsAreShowing3 = true;
        }
    }

    private IEnumerator showArrows(int a)
    {
        for (int i = 0; i <= a; i++)
        {
            arrowArray[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
