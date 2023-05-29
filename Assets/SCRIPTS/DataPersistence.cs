using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataPersistence : MonoBehaviour
{
    public static DataPersistence sharedInstance;
    public string username;
    public int completedLevels = 0;



    private void Awake()
    {
        if (sharedInstance == null)
        {
            sharedInstance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }
    }
}
