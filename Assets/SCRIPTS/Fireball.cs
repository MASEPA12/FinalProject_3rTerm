using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private int damage = -1; //Negative, indicates the damage done to the player
    private float knockback = 800f; //Knockback done to the player

    private float upAttackForce = 150f; //applied force to the bullet
    private float forwardAttackForce = 800f;

    private float inactiveTimer = 1f; //time to set the gameobject to inactive

    private void OnEnable()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = Vector3.zero; //reset
        //Applied Force
        rigidbody.AddForce(transform.forward * forwardAttackForce, ForceMode.Impulse);
        //rigidbody.AddForce(transform.up * upAttackForce, ForceMode.Impulse);
        Invoke("SetInactive", inactiveTimer);
    }
}
