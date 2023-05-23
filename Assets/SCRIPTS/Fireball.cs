using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Fireball : MonoBehaviour
{

    //private float upAttackForce = 150f; //applied force to the bullet
    private float forwardAttackForce = 300f;

    private float inactiveTimer = 1f; //time to set the gameobject to inactive

    //Components
    private Rigidbody rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        //Applied Force
        rb.AddRelativeForce(transform.forward * forwardAttackForce,ForceMode.Impulse);
        //rigidbody.AddForce(transform.up * upAttackForce, ForceMode.Impulse);
        Invoke("SetInactive", inactiveTimer);
    }

    private void SetInactive()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        gameObject.SetActive(false);
    }
}
