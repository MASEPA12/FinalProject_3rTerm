using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    //capsule 
    private Vector3 centerCapsuleUp;
    private Vector3 centerCapsuleDwn;
    private float radiusCapsule = 0.15f;
    public LayerMask layerMaskToCollide;
    [SerializeField] private float maxDistance = 0.1f;

    private Vector3 playerDirection;

    //box collider refernce
    public BoxCollider boxColliderPlayer;

    //bool system
    private bool isColliding;
    private bool somethingIsOn;
    private bool onFloor;

    //script references
    private PlayerMovement playerMovementScript;

    //collisions
    private Vector3 previousPos;

    void Start()
    {
        playerMovementScript = FindObjectOfType<PlayerMovement>();   
    }

    void Update()
    {   //set the size of the capsule
        centerCapsuleDwn = transform.position + Vector3.up * ( radiusCapsule);
        centerCapsuleUp = transform.position + ((boxColliderPlayer.size.y - radiusCapsule) * Vector3.up);

        playerDirection = new Vector3(playerMovementScript.horizontalInput, 0, playerMovementScript.forwardInput).normalized;

        isColliding = Physics.CapsuleCast(centerCapsuleDwn, centerCapsuleUp, radiusCapsule, -playerDirection, maxDistance, layerMaskToCollide);

        if (isColliding)
        {
            transform.position = previousPos;
        }
        else
        {
            previousPos = transform.position;
        }

        //Check if there is something over the player
        somethingIsOn = Physics.Raycast(transform.position + Vector3.up * boxColliderPlayer.size.y, (transform.position + Vector3.up * boxColliderPlayer.size.y) + Vector3.up * 0.25f, maxDistance, layerMaskToCollide);
        if (somethingIsOn)
        {
            playerMovementScript.canBeSteady = false;            
        }
        else
        {
            playerMovementScript.canBeSteady = true;   
        }

        //Check if player hitted the floor
        RaycastHit hit;
        onFloor = Physics.Raycast(transform.position,Vector3.down, out hit,boxColliderPlayer.size.y/2*0.2f);
        if (onFloor)
        {
            playerMovementScript.CanJump(true);
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

        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(playerDirection, playerDirection + new Vector3 (0,0,1));

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * boxColliderPlayer.size.y, (transform.position + Vector3.up * boxColliderPlayer.size.y) + Vector3.up * 0.25f);

        //
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down*boxColliderPlayer.size.y / 2 * 0.2f);
    }
}
