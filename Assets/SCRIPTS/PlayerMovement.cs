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
    public Rigidbody rb;
    public float walkingForce = 0.5f;

    public float jumpingForce = 0.2f;
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
    GameManager gameManagerScript;


    //recollectable variables
    private int breadPoints = 1;
    private int meatPoints = 5;

    //power ups
    public int secondsToWaitAppleRed = 5;

    //actual position
    public Vector3 pos;

    //sphere
    public float sphereRadius1 = 5;
    public float sphereRadius2 = 10;

    public bool isInTheSphere = false;
    public LayerMask recollectableLayerMask;
    public LayerMask powerUpsLayerMask;


    void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>();

        animator = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        rb = GetComponent<Rigidbody>();
        animator.SetBool("isSteady", isSteady);

        gameManagerScript.counterSliderPanel.SetActive(false);
        gameManagerScript.appleRedIsOn = false;

        gameManagerScript.isBig = false;

        pos = transform.position;

        StartCoroutine(gameManagerScript.LooseFoodTimer());
    }

    private void Update()
    {
        //CROUCH
        if (Input.GetKey(KeyCode.E))
        {
            isSteady = false;
        }
        else
        {
            isSteady = true;
        }

        //JUMPING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
        }
        else
        {
            isJumping = false;
        }

        //RUNNING
        if (Input.GetKey(KeyCode.W))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }

        //PATADA
        if (Input.GetKeyDown(KeyCode.F))
        {
            isKicking = true;
        }
        else
        {
            isKicking = false;
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

        //RUNNING BCK
        if (Input.GetKey(KeyCode.S))
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

        gameManagerScript.DestroyRecollectable(other, meet, meatPoints);
        gameManagerScript.DestroyRecollectable(other, bread, breadPoints);

        if (other.CompareTag(appleRed) && gameManagerScript.appleRedIsOn == false) //si ja te es power up de sa poma vermella on, no n'agafa més
        {
            Debug.Log("entre");
            Destroy(other.gameObject);

            StartCoroutine(gameManagerScript.LocalScaleTransformer(secondsToWaitAppleRed));

        }

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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")){
            
            //knockback dirrection
            Vector3 pushAway = (transform.position - collision.gameObject.transform.position).normalized; //Get direction back to be pushed
            //Update hearts
            takeDamage(-1, 700f, pushAway);
        }
    }

    //Function that manages de damage done to the player
    public void takeDamage(int damage, float knockback, Vector3 knockbackDir) {
        gameManagerScript.UpdateLife(damage);
        //Apply knockback
        rb.AddForce(knockbackDir * knockback, ForceMode.Impulse); //Knockback
        //play auchh sound
    }

    public void restoreLife() {
        gameManagerScript.UpdateLife(1); //Restore 1 point
    }

}

