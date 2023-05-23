using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class prova1UI : MonoBehaviour
{
    public Image[] arrowArray;

    //aquests bools les necessitam per fer lock/unlock als nivells, no se exactament a quin script anirien
    public bool hasWonLev1;
    public bool hasWonLev2;
    public bool hasWonLev3;

    public int i;

    void Update()
    {
        if(hasWonLev1 == true)
        {
            //has to show 4 arrows
            StartCoroutine(showArrows(4));
        }
        if(hasWonLev2 == true)
        {
            //has to show 4 arrows
            StartCoroutine(showArrows(9));
        }
        if(hasWonLev3 == true)
        {
            //has to show all the arrows
            StartCoroutine(showArrows(arrowArray.Length));
        }
    }

    private IEnumerator showArrows(int i)
    {
        for (i = 0; i <= 4; i++)
        {
            Debug.Log("un pic");
            arrowArray[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
