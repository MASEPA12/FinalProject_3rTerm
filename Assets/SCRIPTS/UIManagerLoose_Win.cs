using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


/*script for change the player name's when looses*/
public class UIManagerLoose_Win : MonoBehaviour
{
    public TextMeshProUGUI looseText;
    public TextMeshProUGUI winText;

    private Scene currentScene;
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
        //if the scene  were we are is the loose scene (buildIndex = 4)
        if (currentScene.buildIndex == 5)
        {
            looseText.text = $"{DataPersistence.sharedInstance.username}, you have lost";
        }
        else if(currentScene.buildIndex == 4) //otherwise, if we are in the win scene
        {
            winText.text = $"{DataPersistence.sharedInstance.username}, you won!!";
        }
    }
    public void ReturnToScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(DataPersistence.sharedInstance.completedLevels);

    }
}
