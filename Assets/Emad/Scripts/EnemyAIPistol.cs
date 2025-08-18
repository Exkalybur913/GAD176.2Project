using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIPistol : MonoBehaviour
{  /// <summary>// This script controls an enemy AI that uses a pistol to shoot at the player when detected.
/// </summary>
    [Header("Vision Settings")]
    [SerializeField] private float detectionRadius = 20f;
    [SerializeField] private float visionAngle = 60f;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private LayerMask obstructionMask;

    [Header("Combat")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireCooldown = 1.5f;
    [SerializeField] private float bulletSpeed = 20f;

    private float lastFireTime;

    private void Update()
    {
        if (CanSeePlayer(out Transform player)) // Check if the player is visible
        {
            transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));

            if (Time.time > lastFireTime + fireCooldown) // Check if the enemy can fire again
            {
                ShootAtPlayer(player);
                lastFireTime = Time.time;
            }
        }
    }

    private bool CanSeePlayer(out Transform playerTransform)
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, playerLayer);
        foreach (Collider hit in hits) // Check for player within detection radius
        {
            Vector3 dirToPlayer = (hit.transform.position - transform.position).normalized;
            float angle = Vector3.Angle(transform.forward, dirToPlayer);// Calculate angle to player

            if (angle < visionAngle / 2f)
            {
                float distanceToPlayer = Vector3.Distance(transform.position, hit.transform.position);// Calculate distance to player

                if (!Physics.Raycast(transform.position, dirToPlayer, distanceToPlayer, obstructionMask))
                {
                    playerTransform = hit.transform;
                    return true;// Player is visible and not obstructed
                }
            }
        }

        playerTransform = null;
        return false;
    }

    private void ShootAtPlayer(Transform player) // Fire a bullet towards the player
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
        Vector3 dir = (player.position - firePoint.position).normalized; // Calculate direction to player

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)// Check if the bullet has a Rigidbody component
        {
            rb.velocity = dir * bulletSpeed;
        }

        Debug.Log("Enemy fired at player.");
    }
  
}