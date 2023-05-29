using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    /*
    private void OntriggerEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Debug.Log("Estas");
            collision.transform.SetParent(transform);
        }
            
    }
    */
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Sales");
            collision.transform.SetParent(null);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entras");
            collision.transform.SetParent(transform);
        }
    }
    /*
    private void OnCollisionEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Estas");
            other.transform.SetParent(transform);
        }     
    }
    */

    /*
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Sales");
            other.transform.SetParent(null);
        }
    }
    */
}
