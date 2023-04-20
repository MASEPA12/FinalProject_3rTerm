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
    //private bool isJumping;
    public Animator animator;

    //Animation Booleans
    private bool isJumping = false;
    private bool isSteady = true;
    private bool isRunning = false;
    private bool isRuningBack = false;
    private bool isKicking = false;
    private bool isShotting = false;

    void Start()
    {
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
        if (!Input.GetKey(KeyCode.E))
        {
            isSteady = true;
        }

        //JUMPING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
        }
        if (!Input.GetKey(KeyCode.Space))
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
        if (!Input.GetKeyDown(KeyCode.F))
        {
            isKicking = false;
        }

        //SHOOTING
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isShotting = true;
        }
        if (!Input.GetKeyDown(KeyCode.Q))
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
        
        /*
        movement = new Vector3(horizontalInput, 0, forwardInput);
        transform.Translate(movement * walkingForce * Time.deltaTime);
        */

        rb.MovePosition(transform.position + walkingForce * Time.deltaTime * forwardInput * transform.forward); //Move forward
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
        
        /*
        //CROUCH
        if (Input.GetKey(KeyCode.E))
        {
            animator.SetBool("isSteady", false); //ja no esta`dret  
            animator.SetBool("isRunning", false); //ja no esta`dret  
        }
        if (!Input.GetKey(KeyCode.E))
        {
            animator.SetBool("isSteady", true); //si no pitj, està dret
            //rb.AddForce(Vector3.forward * walkingForce, ForceMode.Force);
        }

        //JUMPING
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);
            animator.SetBool("isJumping", true);
        }
        if (!Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("isJumping", false);
        }

        //RUNNING
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        //PATADA
        if (Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("isPegando", true);

        }
        if (!Input.GetKeyDown(KeyCode.F))
        {
            animator.SetBool("isPegando", false);
        }

        //SHOOTING
        if (Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("isShooting", true);
        }
        if (!Input.GetKeyDown(KeyCode.Q))
        {
            animator.SetBool("isShooting", false);
        }

        //RUNNING BCK
        if (Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isRunningBck", true);
        }
        else
        {
            animator.SetBool("isRunningBck", false);
        }
        */
    }

}

