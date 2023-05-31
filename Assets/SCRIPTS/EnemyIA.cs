using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    //Range Variables
    private NavMeshAgent _agent;
    public Transform player;
    public float visionRange;
    public bool playerInVisionRange;

    //Attack Variables
    private bool canAttack = true;
    [SerializeField] private int damage = -1; //damage that does the enemy
    public float attackCooldownTimer;
    public float attackRange;
    public bool playerInAttackRange;
    [SerializeField] private float knockback = 200f;
    //Waypoints Variables
    public Transform[] waypoints;
    public int nextPoint;
    public int totalWaypoints;

    //Speed Variables
    [SerializeField] private float speedPatrol = 3.5f;
    [SerializeField] private float speedChase = 4.5f;

    //Mask
    public LayerMask playerLayer;

    //Scripts connections
    private PlayerMovement playerCon;

    //Item
    [SerializeField] private GameObject item;

    //
    public Animator enemyAnimator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        enemyAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCon = FindObjectOfType<PlayerMovement>();
        totalWaypoints = waypoints.Length;
        nextPoint = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.sharedInstance.IsFinished())
        {
            Vector3 pos = transform.position;
            playerInVisionRange = Physics.CheckSphere(pos, visionRange, playerLayer);
            playerInAttackRange = Physics.CheckSphere(pos, attackRange, playerLayer);

            if (!playerInVisionRange && !playerInAttackRange)
            {
                Patrol();
            }

            if (playerInVisionRange && !playerInAttackRange)
            {
                Chase();
            }

            if (playerInVisionRange && playerInAttackRange)
            {
                Attack();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    //Function that manages enemy patrol 
    private void Patrol() 
    {
        enemyAnimator.SetBool("playerOutOfRange",true);

        _agent.speed = speedPatrol;
        if (Vector3.Distance(transform.position, waypoints[nextPoint].position) < 2.5) {
            nextPoint++;
            if (nextPoint == totalWaypoints) {
                nextPoint = 0;
            }
            transform.LookAt(waypoints[nextPoint].position);
        }

        _agent.SetDestination(waypoints[nextPoint].position);
    }

    //Function that manages enemy chase
    private void Chase() 
    {
        enemyAnimator.SetBool("playerOnChaseRange", true);

        _agent.speed = speedChase;
        _agent.SetDestination(playerCon.transform.position);
        transform.LookAt(playerCon.transform);
    }

    //Function that manages enemy Attack
    private void Attack() 
    {
        enemyAnimator.SetBool("playerOnAttakRange", true);

        if (canAttack) {
            //Play attack animation
            canAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    //alomejor moverlo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //knockback dirrection
            Vector3 pushAway = (collision.gameObject.transform.position - transform.position).normalized; //Get direction back to be pushed
            //Update hearts
            playerCon.takeDamage(damage, knockback, pushAway);
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            enemyAnimator.SetBool("isDead", true);

            //play particle explosion
            DropItem();
            Destroy(gameObject); //Destroy enemy
        }
    }

    //Coroutine that manages the attack cooldown
    private IEnumerator AttackCooldown() {   
        yield return new WaitForSeconds(attackCooldownTimer);
        Debug.Log("Preparese para la ostia");
        canAttack = true;
    }

    private void DropItem() {
        float prob = Random.Range(0f, 1f);
        Vector3 itemSpawn= new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        if (prob > 0.3f) {
            Instantiate(item, itemSpawn, Quaternion.identity);
        }
    }

}
