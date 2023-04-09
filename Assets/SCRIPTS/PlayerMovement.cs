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

    //private bool isJumping;
    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        rb = GetComponent<Rigidbody>();
        //isJumping = false;

    }

    private void Update()
    {
        bool crouchPressed = Input.GetKey("c");
        forwardInput = Input.GetAxis("Vertical"); //Check if W or S is pressed
        horizontalInput = Input.GetAxis("Horizontal"); //Check if A o D is pressed

        if (crouchPressed)
        {
            animator.SetBool("isSteady", false); //ja no està dret 

        }
        if (!crouchPressed)
        {
            animator.SetBool("isSteady", true); //si no pitj, està dret
            //rb.AddForce(Vector3.forward * walkingForce, ForceMode.Force);

        }

        rb.AddForce(transform.forward * walkingForce * forwardInput); //Move forward
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * horizontalInput);//rotate body

        //IN progress
        if (forwardInput != 0)
        {
            animator.SetBool("isRunning", true);
        }
        else {
            animator.SetBool("isRunning", false);
        }
        

        if (Input.GetKeyDown(KeyCode.Space)) //bota
        {
            rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);

            animator.SetBool("isJumping", true);

        }

        if (!Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("isJumping", false);
        }

    }
}
