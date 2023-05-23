using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = -1; //Negative, indicates the damage done to the player
    private float knockback = 800f; //Knockback done to the player

    //private float upAttackForce = 150f; //applied force to the bullet
    private float forwardAttackForce = 800f;

    private float inactiveTimer = 1f; //time to set the gameobject to inactive
    
    //Script connections
    private GameManager gameManager;
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
        gameManager = FindObjectOfType<GameManager>();
        playerCon = FindObjectOfType<PlayerMovement>();
    }

    private void OnEnable()
    {
        //Applied Force
        rb.AddRelativeForce(transform.forward * forwardAttackForce, ForceMode.Impulse);
        //rigidbody.AddForce(transform.up * upAttackForce, ForceMode.Impulse);
        Invoke("SetInactive", inactiveTimer);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 pushAway = (collision.gameObject.transform.position - transform.position).normalized; //Get direction back to be pushed
            playerCon.takeDamage(damage, knockback, pushAway);
        }

        //gameObject.SetActive(false); //set inactive if collides
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
