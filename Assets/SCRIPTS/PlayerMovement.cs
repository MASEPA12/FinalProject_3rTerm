using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

/*
 Script that controls the player movement, animation and 
interaction with other object
 */

public class PlayerMovement : MonoBehaviour
{
    //Consta
    private const float INITIAL_SPEED = 5f;
    
    //Movement
    private Rigidbody rb;
    private float walkingForce = INITIAL_SPEED;
    [SerializeField] private float jumpingForce = 250f;
    private float gravityModifier = 1.7f;
    private Vector3 gravityForce = new Vector3(0, -9.8f, 0);

    //Speed variables
    public float forwardInput; //Forward and backwar move
    public float horizontalInput; //Rotation movement

    [SerializeField] private float rotationSpeed = 6.5f; //Rotation speed


    private Animator animator;

    //Animation Booleans
    private bool isJumping = false;
    public bool isSteady = true;
    private bool isRunning = false;
    private bool isRuningBack = false;
    private bool isShotting = false;

    //scripts conections
    private PlayerLife playerLife;
    private PowerUp powerUpScript;

    //Game Variables
    private bool canDamage = true; //bool that indicate if the player can receive damage
    private float invulnerabilityTime = 2.5f; //time once the player receive damages that is invulnerable

    //power ups
    private int secondsToWaitAppleRed = 5;
    private int secondsToWaitAppleGreen = 5;

    //Spawn variables
    private Vector3 spawnPos;

    [SerializeField] private LayerMask recollectableLayerMask;
    [SerializeField] private LayerMask powerUpsLayerMask;

    //jumping bool
    [SerializeField] private bool canJump = true;
    public bool canBeSteady = true;

    //Attack
    private bool canAttack = true;
    private float attackTimer = 1.5f;
    [SerializeField] private Transform throwPos;

    void Start()
    {
        powerUpScript = FindObjectOfType<PowerUp>();
        playerLife = FindObjectOfType<PlayerLife>();
        animator = GetComponent<Animator>();

        Physics.gravity = gravityForce*gravityModifier;//

        rb = GetComponent<Rigidbody>();
        animator.SetBool("isSteady", isSteady);

        powerUpScript.counterSliderPanel.SetActive(false);
        powerUpScript.appleRedIsOn = false;

        canJump = true;

        spawnPos = transform.position; //set spawn position at the start of the level;
    }

    private void Update()
    {
        if (!GameManager.sharedInstance.IsFinished()) {
            
            forwardInput = Input.GetAxis("Vertical"); //movement front/back 
            horizontalInput = Input.GetAxis("Horizontal"); //side movement

            //CROUCH
            if (Input.GetButton("Fire1") || (!Input.GetButton("Fire1") && !canBeSteady)) //Fire1 = LCrt || 
            {
                isSteady = false;
                Debug.Log("isSteady == false");

            }
            else if(canBeSteady == true)
            {
                isSteady = true;
            }

            //JUMPING [if the jump input is not equal to 0, it's jumping]
            if (Input.GetButtonDown("Jump") && canJump)
            {
                isJumping = true;
                rb.AddRelativeForce(Vector3.up * jumpingForce, ForceMode.Impulse);
                canJump = false;
                MusicManager.sharedInstance.JumpSound();
            }
            else {
                isJumping = false;
            }

            //RUNNING [if its a positive value, is going forwards]
            if (Input.GetAxis("Vertical") >0)
            {
                isRunning = true;
            }
            else
            {
                isRunning = false;
            }


            //RUNNING BCK [if its a negative value, is going backwards]
            if (Input.GetAxis("Vertical") < 0) 
            {
                isRuningBack = true;
            }
            else
            {
                isRuningBack = false;
            }

            //Shooting
            if (Input.GetButtonDown("Fire2")) {
                Attack();
            }
        }

    }

    private void FixedUpdate()
    {
        if (!GameManager.sharedInstance.IsFinished())
        {
            Vector3 dir = new Vector3(horizontalInput, 0, forwardInput);

            if (forwardInput < 0 || !isSteady)
            {
                rb.MovePosition(transform.position + (walkingForce / 2) * Time.deltaTime * forwardInput * transform.forward); //Move forward
            }
            else
            {
                rb.MovePosition(transform.position + walkingForce * Time.deltaTime * forwardInput * transform.forward); //Move forward
            }
            rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up * rotationSpeed * horizontalInput * Time.deltaTime));//rotate body   
        }
    }

    private void LateUpdate()
    {
        if (!GameManager.sharedInstance.IsFinished())
        {
            animator.SetBool("isSteady", isSteady);
            animator.SetBool("isRunning", isRunning);
            animator.SetBool("isJumping", isJumping);
            animator.SetBool("isShooting", isShotting);
            animator.SetBool("isRunningBck", isRuningBack);
            
            /*
            if (isJumping) {
                canJump = false;
            }
            */
        }

    }

    public void Scale(float num)
    {
        transform.localScale = new Vector3(num, num, num);
    }

    private void OnTriggerEnter(Collider other)
    {
        string appleRed = "appleRed";
        string appleGreen = "appleGreen";
        
        if (other.CompareTag("Collectable")) {
            MusicManager.sharedInstance.RecollectSound();
            RecollectableMovement collectable = other.GetComponent<RecollectableMovement>(); //Get recollectable information

            GameManager.sharedInstance.UpdateScore(collectable.points);
            playerLife.UpdateHunger(collectable.hunger);
            playerLife.UpdateLife(collectable.heal);

            Destroy(other.gameObject);    
        }

        if (other.CompareTag(appleRed) && powerUpScript.appleRedIsOn == false) //si ja te es power up de sa poma vermella on, no n'agafa més
        {
            Destroy(other.gameObject);

            StartCoroutine(powerUpScript.LocalScaleTransformer(secondsToWaitAppleRed));
        }

        if (other.CompareTag(appleGreen) && powerUpScript.appleGreenIsOn == false) //si ja te es power up de sa poma vermella on, no n'agafa més
        {
            Destroy(other.gameObject);

            StartCoroutine(powerUpScript.SpeedPowerUp(walkingForce*2,secondsToWaitAppleGreen));
        }

        
        //If player arrives in the finish line
        if (other.CompareTag("Finish")){//&& points >= 100) {
            GameManager.sharedInstance.IsHasWin();
        }

        
    }
    private void OnCollisionEnter(Collision collision)
    {
        //if the player collides with the pink spikes
        if (collision.gameObject.CompareTag("deathObstacles"))
        {
            playerLife.CheckRetry(); //Retry or game over
        }
    }

    //Function that manages the damage done to the player
    public void takeDamage(int damage, float knockback, Vector3 knockbackDir) {

        if (canDamage)
        {
            playerLife.UpdateLife(damage);
            //Apply knockback
            rb.AddForce(Vector3.up * 1, ForceMode.Impulse);
            rb.AddForce(knockbackDir * knockback, ForceMode.Impulse); //Knockback
            MusicManager.sharedInstance.DamageSound();
        }
        canDamage = false;
        StartCoroutine(InvincibleTime());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Checkpoint")) {
            spawnPos = transform.position; //update to new spawn position;
        }

        if (other.CompareTag("floor")){
            playerLife.CheckRetry();
        }
    }

    //Coroutine that show the time the player is invincible
    private IEnumerator InvincibleTime() {
        //feedback to player
        yield return new WaitForSeconds(invulnerabilityTime);
        canDamage = true;
    }

    public void ChangeSpeed(float newSpeed) {
        walkingForce = newSpeed;
    }

    public void SetInitialSpeed() {
        walkingForce = INITIAL_SPEED;
    }


    private void Attack()
    {
        if (canAttack)
        {
            //Request bullet from BulletPool
            GameObject fireball = FireballPool.Instance.Request();
            //Get reposition of the bullet
            fireball.transform.position = throwPos.position;         
            fireball.transform.rotation = throwPos.rotation;
            fireball.GetComponent<Rigidbody>().AddForce(transform.forward * 250, ForceMode.Impulse);
            MusicManager.sharedInstance.ThrowSound();
            canAttack = false;
            StartCoroutine(AttackCooldown()); //Start attack cooldown
        }
    }

    //Coroutine that manages the attack cooldown
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackTimer);
        canAttack = true;
    }

    //Function that
    public void CanJump(bool state) {
        canJump = state;
    }

    //Function that moves the player to the spawnPosition
    public void ResetPosition()
    {
        transform.position = spawnPos;
    }

}

