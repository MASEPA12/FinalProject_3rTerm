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
    public bool canAttack = true;
    public int damage = -1; //damage that does the enemy
    public float attackCooldownTimer;
    public float attackRange;
    public bool playerInAttackRange;
    
    //Waypoints Variables
    public Transform[] waypoints;
    public int nextPoint;
    public int totalWaypoints;

    //Speed Variables
    [SerializeField] float speedPatrol = 3.5f;
    [SerializeField] float speedChase = 4.5f;

    //Mask
    public LayerMask playerLayer;

    //Scripts connections
    private GameManager gameManager;
    private PlayerMovement playerCon;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerCon = FindObjectOfType<PlayerMovement>();
        totalWaypoints = waypoints.Length;
        nextPoint = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isGameOver || gameManager.isWin)
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
    private void Patrol() {
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
    private void Chase() {
        _agent.speed = speedChase;
        _agent.SetDestination(playerCon.transform.position);
        transform.LookAt(playerCon.transform);
    }

    //Function that manages enemy Attack
    private void Attack() {
        if (canAttack) {
            //Play attack animation
            Debug.Log("Fuiste atacado");
            canAttack = false;
            StartCoroutine(AttackCooldown());
        }
    }

    //alomejor moverlo
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {

            //knockback dirrection
            Vector3 pushAway = (collision.gameObject.transform.position - transform.position).normalized; //Get direction back to be pushed
            //Update hearts
            playerCon.takeDamage(damage, 700f, pushAway);
        }
    }

    //Coroutine that manages the attack cooldown
    private IEnumerator AttackCooldown() {   
        yield return new WaitForSeconds(attackCooldownTimer);
        Debug.Log("Preparese para la ostia");
        canAttack = true;
    }
   
}
