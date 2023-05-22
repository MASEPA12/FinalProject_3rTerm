using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DataPersistence : MonoBehaviour
{
    public static DataPersistence sharedInstance;
    public string username;
    public int availableLevels = 1;



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
