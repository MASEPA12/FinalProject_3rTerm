using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOplayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float walkingForce = 0.5f;
   
    public float jumpingForce = 0.2f;
    private float gravityModifier = 1.5f;

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
        bool crouchPressed = Input.GetKey("w");

        if (crouchPressed)
        {
            animator.SetBool("isSteady", false); //ja no esta`dret 

        }
        if (!crouchPressed)
        {
            animator.SetBool("isSteady", true); //si no pitj, està dret
            //rb.AddForce(Vector3.forward * walkingForce, ForceMode.Force);

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
        if (Input.GetKey(KeyCode.D))
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}


