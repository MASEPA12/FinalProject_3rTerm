using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private int damage = -1; //Negative, indicates the damage done to the player
    private float knockback = 800f; //Knockback done to the player

    //Script connections
    private GameManager gameManager;
    private PlayerMovement playerCon;



    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerCon = FindObjectOfType<PlayerMovement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector3 pushAway = (collision.gameObject.transform.position - transform.position).normalized; //Get direction back to be pushed
            playerCon.takeDamage(damage, knockback, pushAway);
        }
    }
}
