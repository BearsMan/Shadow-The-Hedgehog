using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    [Header("Enemy Stats")]
    public float speed = 5f;
    private NavMeshAgent agent;
    private Transform target = null;
    public Transform pathway;
    public List<Vector3> patrolPoint = new List<Vector3>(); // Each point will show where the enemies go to.
    private int currentWayPoint;
    private Transform player;
    private Animator animController;
    public float chaseRange = 10f;
    public float attackCoolDown = 1f;
    public float attackRadius;
    public Transform hands;
    public LayerMask playerLayer;
    public enum EnemyStates
    {
        idle, patrol, chase, attack
    }
    public EnemyStates currentState;
    // Start is called before the first frame update
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

    private void SetNextWayPoint()
    {
        agent.SetDestination(patrolPoint[currentWayPoint]);
        currentWayPoint = (currentWayPoint + 1) % patrolPoint.Count;
    }

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

    private void Attack()
    {
        animController.SetTrigger("Punching");
        Debug.Log("Attack");
        StartCoroutine(AttackCoolDown());
        // Set a cooldown.
        Collider[] hitCollider = Physics.OverlapSphere(hands.position, attackRadius, playerLayer);
    }
   
    private void DetectPlayer()
    {
        float distancePlayer = Vector3.Distance(transform.position, player.position);
        if (distancePlayer < chaseRange)
        {
            ChangeState(EnemyStates.chase);
        }
    }
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCoolDown);
        ChangeState(EnemyStates.chase);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere((hands.position, attackRadius));
    }
}
    