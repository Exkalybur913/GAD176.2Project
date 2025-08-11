using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIBehaviorController : MonoBehaviour
{
    //enumstates
    enum AIState
    {
        Investigate,
        ChasePlayer,
        IdleAround,
        Patrolarea
    }
    [SerializeField] AIState currentState;

    //gameObject Stats
    [SerializeField] float triggerTime = 5;
    public Transform enemyPos;
    public float radius = 3f;
    bool playerDetected;



    //enemy behavior states
    public Investigate investigateState;
    public ChasePlayer chasePlayerState;
    public Patrol patrolPlayerState;
    public EnemyAIBehaviorController idlePlayerState;

    void Start()
    {
        currentState = AIState.IdleAround;
        triggerTime = 0f;
    }

    void Update()
    {
        switch (currentState)
        {
            case AIState.Investigate:
                break;
            case AIState.ChasePlayer:
                chasePlayerState.ChaseThePlayer();
                break;
            case AIState.IdleAround:
                idlePlayerState.Idle();
                break;
            case AIState.Patrolarea:
                patrolPlayerState.BeginPatrol();
                break;
        }

        Debug.Log(triggerTime);

    }

    void PlayerDetection(Vector3 enemyPos, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(enemyPos, radius);   
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag ("Player"))
            {
                currentState = AIState.ChasePlayer;
            }
        }
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void Idle()
    {   
        triggerTime += Time.deltaTime;
            if (triggerTime >= 5)
        {
            currentState = AIState.Patrolarea;
            triggerTime = 0f;
        }

    }
 
}
