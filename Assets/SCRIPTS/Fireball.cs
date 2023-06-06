using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fireball : MonoBehaviour
{
    private float inactiveTimer = 1f; //time to set the gameobject to inactive

    private void OnEnable()
    {
        Invoke("SetInactive", inactiveTimer);
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
    }

}
