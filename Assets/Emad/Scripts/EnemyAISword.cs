using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAISword : MonoBehaviour
{
    [Header("Vision Settings")]
    [SerializeField] private float detectionRadius = 15f;
    [SerializeField] private float visionAngle = 70f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstructionMask;

    [Header("Combat")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float attackCooldown = 2f;
    [SerializeField] private Animator animator;

    private Transform player;
    private NavMeshAgent agent;
    private float lastAttackTime;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (CanSeePlayer(out player))
        {
            agent.SetDestination(player.position);

            float distance = Vector3.Distance(transform.position, player.position);
            if (distance <= attackRange && Time.time > lastAttackTime + attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
        else
        {
            agent.ResetPath();
        }
    }

    private bool CanSeePlayer(out Transform playerTransform)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        foreach (Collider hit in hits)
        {
            Vector3 dirToPlayer = (hit.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dirToPlayer);

            if (angle < visionAngle / 2f)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, hit.transform.position);

                if (!Physics.Raycast(transform.position, dirToPlayer, distanceToPlayer, obstructionMask))
                {
                    playerTransform = hit.transform;
                    return true;
                }
            }
        }

        playerTransform = null;
        return false;
    }

    private void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
            DealSwordDamage();

        }

        Debug.Log("Enemy sword attack triggered.");
    }
    public void DealSwordDamage()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) <= attackRange)
        {
            IDamageable damageable = player.GetComponent<IDamageable>();
            if (damageable != null)
            {
                damageable.TakeDamage(3); // Or whatever sword damage
            }
        }
    }
}