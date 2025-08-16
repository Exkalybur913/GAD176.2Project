using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ranged
{

    public class NewAI : MonoBehaviour
    {

        [Header("Vision Settings")]
        public float detectionRange = 15f;
        public float visionAngle = 60f;
        public Transform eyeOrigin;

        [Header("Shooting Settings")]
        public GameObject bulletPrefab;
        public Transform firePoint;
        public float fireRate = 1f;
        private float nextFireTime = 0f;
        public float bulletSpeed = 10f;

        [Header("Target")]
        private Transform player;

        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        void Update()
        {
            if (player == null) return;

            if (IsPlayerInSight())
            {
                ShootAtPlayer();
            }
        }

        bool IsPlayerInSight()
        {
            Vector3 directionToPlayer = player.position - eyeOrigin.position;
            float distanceToPlayer = directionToPlayer.magnitude;

            // Check range
            if (distanceToPlayer > detectionRange)
                return false;

            // Check angle
            float angle = Vector3.Angle(eyeOrigin.forward, directionToPlayer);
            if (angle > visionAngle / 2f)
                return false;

            // Check line of sight
            Ray ray = new Ray(eyeOrigin.position, directionToPlayer.normalized);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, detectionRange))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true;
                }
            }

            return false;
        }

        void ShootAtPlayer()
        {
            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(player.position - firePoint.position));
                bulletPrefab.GetComponent<Rigidbody>().AddForce(bulletPrefab.transform.forward * bulletSpeed, ForceMode.Impulse);
                Debug.Log("Enemy shot at player!");
            }
        }
    }
}