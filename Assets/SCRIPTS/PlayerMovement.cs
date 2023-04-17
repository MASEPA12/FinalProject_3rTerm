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

    void Start()
    {
        animator = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        rb = GetComponent<Rigidbody>();
        //isJumping = false;

    }

    private void FixedUpdate()
    {
        forwardInput = Input.GetAxis("Vertical"); //movement front/back 
        horizontalInput = Input.GetAxis("Horizontal"); //side movement

        /*
        movement = new Vector3(horizontalInput, 0, forwardInput);
        transform.Translate(movement * walkingForce * Time.deltaTime);
        */
        
        rb.AddForce(transform.forward * walkingForce * forwardInput); //Move forward
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * horizontalInput);//rotate body
        
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
    }

}

