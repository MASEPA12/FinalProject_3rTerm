using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform playerPos; //get player position
    public float visionRange;
    public bool playerInVisionRange;
    public LayerMask playerLayer;

    // Start is called before the first frame update
    void Start()
    {
        //playerPos = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        playerInVisionRange = Physics.CheckSphere(pos, visionRange, playerLayer);

        if (playerInVisionRange) {
            lookAtPlayer();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    private void lookAtPlayer() {
        Vector3 lookAtPlayer = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z);
        transform.LookAt(lookAtPlayer);
    }
}
