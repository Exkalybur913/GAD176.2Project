using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDamageable;


public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 20f;
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private int damage = 10;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject, lifetime); // Auto-destroy after time
    }

    public void Fire(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            Debug.Log($"Enemy hit."); ;
        }

        Destroy(gameObject); // Destroy bullet on hit
    }
}
