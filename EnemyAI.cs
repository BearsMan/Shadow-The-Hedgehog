using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    public float speed = 5f;
    private NavMeshAgent agent;
    private Transform target = null;
    public enum enemyStates
    {
        patrol, attack, idle, chase,
    }
    public enemyStates currentState;
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
            agent.SetDestination(target.position);
        }
        switch (currentState)
        {
            case enemyStates.patrol:
                break;

            case enemyStates.attack:
                break;

            case enemyStates.idle:
                break;

            case enemyStates.chase:
                break;
        }
    }
}
