using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    //capsule 
    public Vector3 centerCapsuleUp;
    public Vector3 centerCapsuleDwn;
    public float radiusCapsule;
    public LayerMask layerMaskToCollide;
    public float maxDistance;

    public Vector3 playerDirection;
    public float playerHigh;

    //box collider refernce
    public BoxCollider boxColliderPlayer;

    //bool system
    public bool isColliding;
    private bool somethingIsOn;

    //script references
    public PlayerMovement playerMovementScript;

    //collisions
    private Vector3 previousPos;

    void Start()
    {
        playerMovementScript = FindObjectOfType<PlayerMovement>();   
    }

    void Update()
    {   //set the size of the capsule (posaré sa de abaix un pos més amunt pq no estigui tot es temps tocant enterra, pq s'enveoirment és tb ground) PENSAR A LLEVAR ES +0.5f
        centerCapsuleDwn = transform.position + Vector3.up * ( radiusCapsule + 0.5f);
        centerCapsuleUp = transform.position + ((boxColliderPlayer.size.y - radiusCapsule) * Vector3.up);

        playerDirection = new Vector3(playerMovementScript.horizontalInput, 0, playerMovementScript.forwardInput).normalized;

        isColliding = Physics.CapsuleCast(centerCapsuleDwn, centerCapsuleUp, radiusCapsule, playerDirection, maxDistance, layerMaskToCollide);

        if (isColliding)
        {
            transform.position = previousPos;
        }
        else
        {
            previousPos = transform.position;
        }

        somethingIsOn = Physics.Raycast(transform.position + Vector3.up * boxColliderPlayer.size.y, (transform.position + Vector3.up * boxColliderPlayer.size.y) + Vector3.up * 0.25f, maxDistance, layerMaskToCollide);
        if (somethingIsOn)
        {
            playerMovementScript.canJump = false;
            if (!playerMovementScript.isSteady)
            {
                playerMovementScript.canBeSteady = false;
            }
           Debug.Log("SOMETHING IS ON THE PLAYER");

        }
        else
        {
            playerMovementScript.canJump = true;
            playerMovementScript.canBeSteady = true;
            Debug.Log("NOTHING ON THE PLAYER");

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(centerCapsuleDwn, radiusCapsule);
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(centerCapsuleUp, radiusCapsule);

        //COMPROVATION WHERE THE FORWARD OF THE PLAYER IS
        Gizmos.color = Color.green;
        Gizmos.DrawLine(centerCapsuleDwn + Vector3.forward * radiusCapsule, centerCapsuleUp + Vector3.forward * radiusCapsule);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * boxColliderPlayer.size.y, (transform.position + Vector3.up * boxColliderPlayer.size.y) + Vector3.up * 0.25f);
    }
}
