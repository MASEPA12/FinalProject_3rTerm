using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NOplayerController : MonoBehaviour
{
    public Rigidbody rb;
    public float walkingForce = 0.5f;
   
    public float jumpingForce = 0.2f;
    private float gravityModifier = 1.5f;

    public AudioSource audioSource;
    public AudioClip prova1;

    //private bool isJumping;
    public Animator animator;

    //public bool isOnTheGround;

    void Start()
    {
        animator = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        //isJumping = false;

    }

    private void Update()
    {
        bool crouchPressed = Input.GetKey("w");

        if (crouchPressed)
        {
            animator.SetBool("isSteady", false); //ja no està dret (està acotat) 
            //isOnTheGround = true;

        }
        if (!crouchPressed)
        {
            animator.SetBool("isSteady", true); //si no pitj, està dret
            //rb.AddForce(Vector3.forward * walkingForce, ForceMode.Force);
           // isOnTheGround = true;


        }

        if (Input.GetKeyDown(KeyCode.Space)) //bota
        {
            rb.AddForce(Vector3.up * jumpingForce, ForceMode.Impulse);

            animator.SetBool("isJumping", true); //does tehe jump
            
            //isOnTheGround = false;

            /*variable isOnTheGround que se tornarà false quan boti, cosa que servirà 
            per queadrar es renou i per fer sa mecànica de privar es doble bot*/
            //play effect 1
        }

        if (!Input.GetKey(KeyCode.Space))
        {
            //isOnTheGround = true;
            animator.SetBool("isJumping", false);

        }
        if (Input.GetKey(KeyCode.D))
        {
            //isOnTheGround = true;
            animator.SetBool("isRunning", true);
        }
        else
        {
            //isOnTheGround = true;
            animator.SetBool("isRunning", false);
        }
    }
    /*
    public IEnumerator Jumping()
    {
        if(isOnTheGround == false)
        {
            new WaitForSeconds(0.5f);
            audioSource.clip = prova1;
            audioSource.Play();
        }
        yield return true;
    }*/
}


