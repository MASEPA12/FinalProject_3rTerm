using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = -1; //Negative, indicates the damage done to the player
    private float knockback = 300f; //Knockback done to the player

    private float inactiveTimer = 1f; //time to set the gameobject to inactive
    
    //Script connections
    private PlayerMovement playerCon;

    //Components
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();    
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCon = FindObjectOfType<PlayerMovement>();
    }

    private void OnEnable()
    {
        Invoke("SetInactive", inactiveTimer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 pushAway = (collision.gameObject.transform.position - transform.position).normalized; //Get direction back to be pushed
            playerCon.takeDamage(damage, knockback, pushAway);
        }
    }

    private void SetInactive() {
        
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
    
}
