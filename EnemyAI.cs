using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public float speed = 5f;
    private NavMeshAgent agent;
    private Transform target = null;
    public Transform pathway;
    public List<Vector3> patrolPoint = new List<Vector3>(); // Each point will show where the enemies go to.
    private int currentWayPoint;
    private Transform player;
    private Animator animController;
    public float chaseRange = 10f;
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
                Chase(); // The emeny will start chasing your character.
                break;

            case EnemyStates.attack:
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
        if (patrolPoint.Count == 0)
        {
            Debug.LogWarning("You do not have any patrol points assigned");
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
        animController.SetBool("Running", (true));
        agent.SetDestination(player.position);
        if (agent.remainingDistance < 0.1f)
        {
            Attack();
        }
    }

    private void Attack()
    {
        animController.SetTrigger("Punching");
    }
}
    /*
    private void DetectPlayer()
    {
        float distancePlayer = Vector3.Distance(transform.position, player.position);
        if (distancePlayer < chaseRange)
        {
            ChangeState(EnemyStates.chase);
        }
    }
}
    */