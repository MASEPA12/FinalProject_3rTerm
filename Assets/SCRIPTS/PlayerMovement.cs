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
    //C
    public const float INITIAL_SPEED = 5f;
    public Rigidbody rb;
    public float walkingForce = INITIAL_SPEED;

    //Movement
    public float jumpingForce = 0.02f;
    private float gravityModifier = 1.7f;
    private float movementForce = 500f;
    private float movementForceB = 350f;
    private float rotationForce= 250f;

    //Speed variables
    public float forwardInput; //Forward and backwar move
    public float horizontalInput; //Rotation movement
    public float mouseX;
    public float rotationSpeed = 6.5f; //Rotation speed

    public Vector3 movement;
    public Animator animator;

    //Animation Booleans
    private bool isJumping = false;
    public bool isSteady = true;
    private bool isRunning = false;
    private bool isRuningBack = false;
    private bool isShotting = false;

    //scripts conections
    private GameManager gameManagerScript;
    private PowerUp powerUpScript;

    //Game Variables
    private bool canDamage = true; //bool that indicate if the player can receive damage
    private float invulnerabilityTime = 2.5f; //time once the player receive damages that is invulnerable

    //power ups
    public int secondsToWaitAppleRed = 5;
    public int secondsToWaitAppleGreen = 5;

    //Spawn variables
    public Vector3 spawnPos;

    //sphere
    public float sphereRadius1 = 5;
    public float sphereRadius2 = 10;

    public bool isInTheSphere = false;
    public LayerMask recollectableLayerMask;
    public LayerMask powerUpsLayerMask;

    //jumping bool
    public bool canJump = true;
    public bool canBeSteady = true;

    //Attack
    public GameObject fireballPrefab;
    public bool canAttack = true;
    public float attackTimer = 1.5f;
    [SerializeField] private Transform throwPos;

    void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>();
        powerUpScript = FindObjectOfType<PowerUp>();

        animator = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        rb = GetComponent<Rigidbody>();
        animator.SetBool("isSteady", isSteady);

        gameManagerScript.counterSliderPanel.SetActive(false);
        gameManagerScript.appleRedIsOn = false;

        gameManagerScript.isBig = false;
        canJump = true;

        spawnPos = transform.position; //set spawn position at the start of the level;

        StartCoroutine(gameManagerScript.LooseFoodTimer());
    }

    private void Update()
    {
        forwardInput = Input.GetAxis("Vertical"); //movement front/back 
        horizontalInput = Input.GetAxis("Horizontal"); //side movement
        mouseX = Input.GetAxis("Mouse X");

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
            
            Debug.Log($"canJump = {canJump}");
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

        //SHOOTING
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isShotting = true;
        }
        else
        {
            isShotting = false;
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

        if (Input.GetButtonDown("Fire2")) {
            Attack();
        }
    }

    private void FixedUpdate()
    {
        Vector3 dir = new Vector3(horizontalInput,0,forwardInput);

        if (forwardInput < 0 || !isSteady)
        {
            //rb.AddRelativeForce(dir*movementForceB);
            rb.MovePosition(transform.position + (walkingForce/2) * Time.deltaTime * forwardInput * transform.forward); //Move forward
        }
        else {
            //rb.AddRelativeForce(dir*movementForce);
            rb.MovePosition(transform.position + walkingForce * Time.deltaTime * forwardInput * transform.forward); //Move forward
        }


        //rb.AddRelativeTorque(Vector3.up * mouseX *rotationForce);
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up* rotationSpeed * horizontalInput*Time.deltaTime));//rotate body                                                                                                                //
    }

    private void LateUpdate()
    {
        animator.SetBool("isSteady", isSteady);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isShooting", isShotting);
        animator.SetBool("isRunningBck", isRuningBack);

        if (isJumping) {
            canJump = false;
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

        //if the player falls loses
        if (other.CompareTag("floor"))
        {
            gameManagerScript.CheckRetry();
            //set active the game over panel
        }

        //If player arrives in the finish line
        if (other.CompareTag("Finish")){//&& points >= 100) {
            gameManagerScript.IsHasWin();
        }
    }

    //Function that manages the damage done to the player
    public void takeDamage(int damage, float knockback, Vector3 knockbackDir) {
        
        if (canDamage)
        {
            gameManagerScript.UpdateLife(damage);
            //Apply knockback
            rb.AddForce(Vector3.up * 1, ForceMode.Impulse);
            rb.AddForce(knockbackDir * knockback, ForceMode.Impulse); //Knockback
                                                                      //play auchh sound
        }
        canDamage = false;
        StartCoroutine(InvincibleTime());
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Checkpoint")) {
            spawnPos = transform.position; //update to new spawn position;
        }
    }

    //Function that restore player health
    public void restoreLife() {
        gameManagerScript.UpdateLife(1); //Restore 1 point
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


    public void CanJump(bool state) {
        canJump = state;
    }

    public void ResetPosition()
    {
        transform.position = spawnPos;
    }

    /*
    private IEnumerator PushAwayTime()
    {
        yield return new WaitForSeconds(2);
    }*/
}

