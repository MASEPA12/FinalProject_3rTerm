using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform playerPos; //get player position
    public float visionRange;
    public bool playerInVisionRange;
    public LayerMask playerLayer;


    // Attack
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletSpawn;
    private float attackCooldownTimer = 2f;
    private bool canAttack = true;
    private float upAttackForce = 150f;
    private float forwardAttackForce = 800f;

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
        Attack();
    }

    private void Attack()
    {
        if (canAttack) {
            Rigidbody rigidbody = Instantiate(bullet, bulletSpawn.position, Quaternion.identity).GetComponent<Rigidbody>(); //crear la bal y coger su rigidbody
            rigidbody.AddForce(transform.forward * forwardAttackForce, ForceMode.Impulse);
            rigidbody.AddForce(transform.up * upAttackForce, ForceMode.Impulse);

            canAttack = false;
            StartCoroutine(AttackCooldown()); //Start attack cooldown
        }
    }

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldownTimer);
        canAttack = true;
    }
}
