using System.Collections;
using System.Collections.Generic;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.AI;

public class DoomEye : MonoBehaviour
{
    private NavMeshAgent agent;
    private Transform target = null;
    private Transform player;
    [SerializeField] private bool canMove = true;
    public enum EnemyStates
    {
        idle, chase
    }
    // Start is called before the first frame update
    public EnemyStates currentState;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
        ChangeState(EnemyStates.chase);
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyStates.idle:
                Idle();
                break;

            case EnemyStates.chase:
                Chase(); // The enemy will start chasing your character.
                break;
        }

    }
    
    private void Chase()
    {
        if (agent.remainingDistance < 0.1f)
        {
            ChangeState(EnemyStates.idle);
        }
        agent.SetDestination(player.position);
    
    }
    private void Idle()
    {
        if (agent.remainingDistance > 0.1f)
        {
            ChangeState(EnemyStates.chase);
        }
    }

    public void ChangeState(EnemyStates newState)
    {
        currentState = newState;
    }
}
