using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Script that returns some object to their initial position when they fall off
 */
public class ReturnPosition : MonoBehaviour
{
    private Vector3 initialPos;
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
    }

    private void ReturnInitialPos() {
        transform.position = initialPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("floor")) {
            ReturnInitialPos();
        }
    }
}
