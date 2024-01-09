using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public float speed = 5f;
    private NavMeshAgent agent;
    private Transform target = null;
    public enum EnemyStates
    {
        idle, patrol, chase, attack
    }
    public EnemyStates currentState;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent != null)
        {
            agent.SetDestination(target.position); // Note: This will show up as an error in the Unity console window,
                                                   // but it can be ignored as it does not do anything.
        }
        switch (currentState)
        {
            case EnemyStates.patrol:
                break;

            case EnemyStates.attack:
                break;

            case EnemyStates.idle:
                break;

            case EnemyStates.chase:
                break;
        }
    }
}
