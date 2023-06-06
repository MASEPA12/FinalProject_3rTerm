using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //private float startBound;
    //private float endBound;
    private Rigidbody rb;
    [SerializeField] private float force = 1f;
    private Vector3 direction;
    [SerializeField] private Transform[] pointsArray;
    [SerializeField] private bool isGoingToEnd = false;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        rb.velocity = transform.forward * force;
        rb.AddForce(transform.forward, ForceMode.Impulse);
    }

    private void FixedUpdate()
    {
        if (isGoingToEnd)
        {
            rb.AddForce(transform.right*force, ForceMode.Impulse);
        }
        else {
            rb.AddForce(-transform.right*force, ForceMode.Impulse);
        }
        
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

        if (other.gameObject.name.Contains("Start")) 
        {
            rb.velocity = Vector3.zero;
            isGoingToEnd = false;
        }

        if (other.gameObject.name.Contains("End"))
        {
            rb.velocity = Vector3.zero;
            isGoingToEnd = true;
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

}
