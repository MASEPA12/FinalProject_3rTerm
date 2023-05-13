using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boulder : MonoBehaviour
{
    private Vector3 initialPos;
    private Vector3 spawnPos;
    
    private int damage = 1; //Damage done to the player
    private float knockback = 1000f; //Knockback done to the player

    private Rigidbody rb;

    //Script connections
    private GameManager gameManager;
    private PlayerMovement playerCon;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        playerCon = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -15) {
            RandomSpawnPosition();
        }
    }

    private void RandomSpawnPosition() {
        spawnPos = new Vector3(Random.Range(-10f, -55f), initialPos.y, initialPos.z);
        transform.position = spawnPos; //move object to the position
        rb.velocity = Vector3.zero;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            Vector3 pushAway = (collision.gameObject.transform.position - transform.position).normalized; //Get direction back to be pushed
            playerCon.takeDamage(damage, knockback, pushAway);
        }
    }
}
