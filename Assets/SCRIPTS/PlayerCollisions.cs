using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisions : MonoBehaviour
{
    //capsule 
    private Vector3 centerCapsuleUp;
    private Vector3 centerCapsuleDwn;
    private float radiusCapsule = 1f;
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
    {   //set the size of the capsule (posaré sa de abaix un pos més amunt pq no estigui tot es temps tocant enterra, pq s'enveoirment és tb ground) PENSAR A LLEVAR ES +0.5f
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

        somethingIsOn = Physics.Raycast(transform.position + Vector3.up * boxColliderPlayer.size.y, (transform.position + Vector3.up * boxColliderPlayer.size.y) + Vector3.up * 0.25f, maxDistance, layerMaskToCollide);
        if (somethingIsOn)
        {
            playerMovementScript.canBeSteady = false;            
           Debug.Log("SOMETHING IS ON THE PLAYER");

        }
        else
        {
            playerMovementScript.canBeSteady = true;   
        }

        RaycastHit hit;
        onFloor = Physics.Raycast(transform.position,Vector3.down, out hit,boxColliderPlayer.size.y/2*0.2f);
        if (onFloor)
        {
            Debug.Log("onFloor");
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
