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
    //Constant
    public const float INITIAL_SPEED = 5f;
    public Rigidbody rb;
    public float walkingForce = INITIAL_SPEED;

    public float jumpingForce = 0.02f;
    private float gravityModifier = 1.7f;

    //Speed variables
    private float forwardInput; //Forward and backwar move
    private float horizontalInput; //Rotation movement
    public float rotationSpeed = 6.5f; //Rotation speed

    public Vector3 movement;
    public Animator animator;

    //Animation Booleans
    private bool isJumping = false;
    private bool isSteady = true;
    private bool isRunning = false;
    private bool isRuningBack = false;
    private bool isKicking = false;
    private bool isShotting = false;

    //scripts conections
    private GameManager gameManagerScript;
    private PowerUp powerUpScript;

    //Game Variables
    private bool canDamage = true; //bool that indicate if the player can receive damage
    private float invulnerabilityTime = 2.5f; //time once the player receive damages that is invulnerable

    //recollectable variables
    private int breadPoints = 1;
    private int meatPoints = 5;

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
    private bool jumpAxisUsing;

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

        spawnPos = transform.position; //set spawn position at the start of the level;

        StartCoroutine(gameManagerScript.LooseFoodTimer());
    }

    private void Update()
    {
        //CROUCH
        if (Input.GetButton("Fire1")) //Fire1 = LCrt || 
        {
            isSteady = false;
        }
        else
        {
            isSteady = true;
        }

        //JUMPING [if the jump input is not equal to 0, it's jumping]
        if (Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
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

        //PATADA
        /*if (Input.GetKeyDown(KeyCode.F))
        {
            isKicking = true;
        }
        else
        {
            isKicking = false;
        }*/

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
    }

    private void FixedUpdate()
    {
        forwardInput = Input.GetAxis("Vertical"); //movement front/back 
        horizontalInput = Input.GetAxis("Horizontal"); //side movement

        if (forwardInput < 0 || !isSteady)
        {
            rb.MovePosition(transform.position + (walkingForce/2) * Time.deltaTime * forwardInput * transform.forward); //Move forward
        }
        else {
            rb.MovePosition(transform.position + walkingForce * Time.deltaTime * forwardInput * transform.forward); //Move forward
        }
        
        rb.MoveRotation(rb.rotation * Quaternion.Euler(Vector3.up* rotationSpeed * horizontalInput*Time.deltaTime));//rotate body        
    }

    private void LateUpdate()
    {
        animator.SetBool("isSteady", isSteady);
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
        animator.SetBool("isPegando", isKicking);
        animator.SetBool("isShooting", isShotting);
        animator.SetBool("isRunningBck", isRuningBack);
    }

    public void Scale(float num)
    {
        transform.localScale = new Vector3(num, num, num);
    }

    private void OnTriggerEnter(Collider other)
    {
        string bread = "bread";
        string meet = "meet";
        string appleRed = "appleRed";
        string appleGreen = "appleGreen";

        gameManagerScript.DestroyRecollectable(other, meet, meatPoints);
        gameManagerScript.DestroyRecollectable(other, bread, breadPoints);

        if (other.CompareTag(appleRed) && powerUpScript.appleRedIsOn == false) //si ja te es power up de sa poma vermella on, no n'agafa més
        {
            Debug.Log("entre");
            Destroy(other.gameObject);

            StartCoroutine(powerUpScript.LocalScaleTransformer(secondsToWaitAppleRed));
        }

        if (other.CompareTag(appleGreen) && powerUpScript.appleGreenIsOn == false) //si ja te es power up de sa poma vermella on, no n'agafa més
        {
            Debug.Log("entre");
            Destroy(other.gameObject);

            StartCoroutine(powerUpScript.SpeedPowerUp(walkingForce*2,secondsToWaitAppleGreen));
        }

        //if the player falls loses
        if (other.CompareTag("floor"))
        {
            gameManagerScript.IsGameOver();
            //set active the game over panel
            Debug.Log("YOU HAVE LOST");
        }

        //If player
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
            rb.AddForce(Vector3.up * 10, ForceMode.Impulse);
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

    public void changeSpeed(float newSpeed) {
        walkingForce = newSpeed;
    }
}

