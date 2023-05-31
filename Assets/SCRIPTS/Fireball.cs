using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fireball : MonoBehaviour
{
    private float inactiveTimer = 1f; //time to set the gameobject to inactive

    //Components
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        //rb.AddForce(transform.forward * forwardAttackForce, ForceMode.Impulse);
    }

    private void OnEnable()
    {
        //Applied Force
        //rb.AddForce(transform.forward * forwardAttackForce,ForceMode.Impulse);
        //rigidbody.AddForce(transform.up * upAttackForce, ForceMode.Impulse);
        Invoke("SetInactive", inactiveTimer);
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
    }

    /*
    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        //transform.rotation = Quaternion.identity;
    }
    */
}
