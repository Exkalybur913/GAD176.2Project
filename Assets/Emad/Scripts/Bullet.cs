using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDamageable;



public class EnemyBullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    [SerializeField] private float speed = 25f;
    [SerializeField] private int damageAmount = 20;
    [SerializeField] private float lifetime = 5f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("EnemyBullet is missing a Rigidbody component.");
        }
    }

    private void Start()
    {
     
    }

    private void OnTriggerEnter(Collider other)
    {
        // Prevent hitting self or other bullets
        if (other.CompareTag("Enemy") || other.gameObject == gameObject)
            return;

        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(1);
        }

        Destroy(gameObject);
    }
}
