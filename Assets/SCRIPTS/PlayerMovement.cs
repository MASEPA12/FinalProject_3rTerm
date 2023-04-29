using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 Script that controls the player movement, animation and 
interaction with other object
 */

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float walkingForce = 0.5f;

    public float jumpingForce = 0.2f;
    private float gravityModifier = 1.5f;

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
    public int points2;

    void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>();

        animator = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        rb = GetComponent<Rigidbody>();
        animator.SetBool("isSteady", isSteady);

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
    private void OnTriggerEnter(Collider other)
    {
        //string bread = "bread";
        //string meet = "meet";

        //això era per provar només
        if (other.CompareTag("bread"))
        {
            Destroy(other.gameObject); //destroy bread prefab
            gameManagerScript.points++; //update food score
            Debug.Log($"has sumat {gameManagerScript.points} punts");
            //play animation de quan menja // particles play
        }
        //gameManagerScript.DestroyRecollectable(other, bread, 1);
        //gameManagerScript.DestroyRecollectable(other, meet, 2);


    }

}

