using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float speed = 5f;
    private NavMeshAgent agent;
    private Transform target = null;
    public Transform pathway;
    public List<Vector3> patrolPoint = new List<Vector3>();
    private int currentWayPoint;
    private Transform player;
    private Animator animController;
    public float chaseRange = 10f;
    public float attackCoolDown = 1f;
    public float attackRadius = 1f;
    public Transform hands;
    public LayerMask playerLayer;
    public enum EnemyStates
    {
        idle, patrol, chase, attack
    }
    // Start is called before the first frame update
    public EnemyStates currentState;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animController = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform;
        GetWayPoint();
        ChangeState(EnemyStates.patrol); // Patrolling are when enemies are moving around until they ball back up again.
    }

    // Update is called once per frame
    void Update()
    {
        if (agent != null)
        {
            // agent.SetDestination(target.position); // Note: This will show up as an error in the Unity console window,
            // but it can be ignored as it does not do anything.
        }
        switch (currentState)
        {
            case EnemyStates.idle:
                break;

            case EnemyStates.patrol:
                Patrol(); // They will start patrolling.
                break;

            case EnemyStates.chase:
                Chase(); // The enemy will start chasing your character.
                break;

            case EnemyStates.attack:
                Attack();
                break;
        }
    }
    public void ChangeState(EnemyStates newState)
    {
        currentState = newState;
    }

    private void GetWayPoint()
    {
        patrolPoint.Clear();
        for (int i = 0; i < pathway.childCount; i++)
        {
            Transform child = pathway.GetChild(i);
            // Debug.Log(child);
            patrolPoint.Add(child.position);
            // Debug.Log(patrolPoint[i]);
        }
    }
    // This is the start of the patrol function.
    private void Patrol()
    {
        animController.SetBool("Walking", true);
        animController.SetBool("Running", (false));
        if (patrolPoint.Count == 0)
        {
            Debug.LogWarning("You do not have any patrol points assigned.");
            return;
        }
        if (agent.remainingDistance < 0.1f)
        {
            SetNextWayPoint();
        }
        float distancePlayer = Vector3.Distance(transform.position, player.position);
        if (distancePlayer < chaseRange)
        {
            ChangeState(EnemyStates.chase);
        }
    }
    // This is the start of the the setting the next way point function.
    private void SetNextWayPoint()
    {
        agent.SetDestination(patrolPoint[currentWayPoint]);
        currentWayPoint = (currentWayPoint + 1) % patrolPoint.Count;
    }
    // This is the start of the chase function.
    private void Chase()
    {
        if (agent.remainingDistance < 0.1f)
        {
            ChangeState(EnemyStates.attack);
        }
        animController.SetBool("Running", (true));
        animController.SetBool("Walking", (false));
        agent.SetDestination(player.position);
        
    }
    // This is the start of the attack function.
    private void Attack()
    {
        animController.SetTrigger("Punching");
       // Debug.Log("Attack");
        StartCoroutine(AttackCoolDown());
        // Set a cooldown.
        Collider[] hitCollider = Physics.OverlapSphere(hands.position, attackRadius, playerLayer);
        // Debug.Log(hitCollider);
        player.GetComponent<PlayerMovement>();
        foreach (Collider collider in hitCollider)
        {
            Debug.Log(collider);
            GameManager.instance.LoseRing(player.position);
        }
    }
   // Detects if the target is following the player.
    private void DetectPlayer()
    {
        float distancePlayer = Vector3.Distance(transform.position, player.position);
        if (distancePlayer < chaseRange)
        {
            ChangeState(EnemyStates.chase);
        }
    }
    // Setting a cool down for each attack being called.
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDown);
        ChangeState(EnemyStates.chase);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hands.position, attackRadius);
    }
}
    