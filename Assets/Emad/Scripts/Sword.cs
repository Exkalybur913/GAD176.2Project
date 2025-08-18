using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Sword : Weapon
{
    [SerializeField] private Animator animator;
    [SerializeField] private string attackAnimationName = "Attack";

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
    }

    public override void Attack()
    {
        if (animator != null)
        {
            animator.Play(attackAnimationName);
        }

        Debug.Log($"{weaponName} swings the sword.");
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damage);
            Debug.Log($"{weaponName} hit {other.name} for {damage} damage.");
        }
    }
}