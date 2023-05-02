using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIA : MonoBehaviour
{
    private NavMeshAgent _agent;
    public Transform player;
    public float visionRange;
    public bool playerInVisionRange;

    public bool canAttack = true;
    public float attackCooldownTimer;
    public float attackRange;
    public bool playerInAttackRange;
    
    public Transform[] waypoints;
    public int nextPoint;
    public int totalWaypoints;

    public LayerMask playerLayer;


    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();   
    }

    // Start is called before the first frame update
    void Start()
    {
        totalWaypoints = waypoints.Length;
        nextPoint = 1;
    }

    // Update is called once per frame
    void Update()
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
        
        if (playerInVisionRange && playerInAttackRange) {
            Attack();
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    private void Patrol() {
        _agent.speed = 3.5f;
        if (Vector3.Distance(transform.position, waypoints[nextPoint].position) < 2.5) {
            nextPoint++;
            if (nextPoint == totalWaypoints) {
                nextPoint = 0;
            }
            transform.LookAt(waypoints[nextPoint].position);
        }

        _agent.SetDestination(waypoints[nextPoint].position);
    }

    private void Chase() {
        _agent.speed = 4.5f;
        _agent.SetDestination(player.position);
        transform.LookAt(player);
    }

    private void Attack() {
        if (canAttack) {
            //Play attack animation
            Debug.Log("Fuiste atacado");
            canAttack = false;
            StartCoroutine(AttackCooldown());

        }
    }

    private IEnumerator AttackCooldown() {
        
        yield return new WaitForSeconds(attackCooldownTimer);
        Debug.Log("Preparese para la ostia");
        canAttack = true;
    }
}
