using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform playerPos; //get player position
    public float visionRange;
    public bool playerInVisionRange;
    public LayerMask playerLayer;


    // Attack Variables
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawn;
    private float attackCooldownTimer = 2f;
    private bool canAttack = true;
    /*
    private float upAttackForce = 150f; //applied force to the bullet
    private float forwardAttackForce = 800f;
    */

    // Start is called before the first frame update
    void Start()
    {
        playerPos = FindObjectOfType<PlayerMovement>().gameObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        playerInVisionRange = Physics.CheckSphere(pos, visionRange, playerLayer);

        if (playerInVisionRange) { //Check if the player is in range
            lookAtPlayer();
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }

    //Function that rotates towards the player
    private void lookAtPlayer() {
        Vector3 lookAtPlayer = new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z);
        transform.LookAt(lookAtPlayer);
        Attack();
    }

    //Function that controls all attack realated instruccions
    private void Attack()
    {
        if (canAttack) {
            //Request bullet from BulletPool
            GameObject bullet = BulletPool.Instance.RequestBullet();
            //Get reposition of the bullet
            bullet.transform.position = bulletSpawn.position;
            bullet.transform.rotation = bulletSpawn.rotation;
            bullet.GetComponent<Rigidbody>().AddRelativeForce(transform.forward * 800, ForceMode.Impulse);
            canAttack = false;
            StartCoroutine(AttackCooldown()); //Start attack cooldown
        }
    }

    //Coroutine that manages the attack cooldown
    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTimer);
        canAttack = true;
    }
}
