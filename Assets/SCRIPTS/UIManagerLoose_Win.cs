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
    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void NextLevel()
    {
        int nextLevel;
        if (currentScene.buildIndex < 3) {
            nextLevel = currentScene.buildIndex + 1;
        }
        else
        {
            nextLevel = 0;
        }
        
        SceneManager.LoadScene(nextLevel);
    }
}
