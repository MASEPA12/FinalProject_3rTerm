using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private float startBound;
    private float endBound;
    private Rigidbody rb;
    [SerializeField] private float force;
    private Vector3 direction;
    [SerializeField] private Transform[] pointsArray;
    [SerializeField] private bool isGoingToEnd = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        
    }

    public void Move(Transform point)
    {
        rb.velocity = point.forward * force;
        direction = point.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Entras");
            other.transform.parent.SetParent(transform);
        }

        if (other.gameObject.name.Contains("start")) 
        {
            isGoingToEnd = true;
        }

        if (other.gameObject.name.Contains("end"))
        {
            isGoingToEnd = false;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Sales");
            other.transform.parent.SetParent(null);
        }
    }


    /*
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
    */

}
