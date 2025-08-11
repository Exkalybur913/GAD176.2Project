using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator animator;
    private AudioSource sliceAudio;

    public float attackRange = 2f;  // How far I can reach for attack
 


    void Start()
    {
        animator = GetComponent<Animator>();
        sliceAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");

            if (sliceAudio != null && sliceAudio.clip != null)
            {
                sliceAudio.Play();
            }
            else
            {
                Debug.LogWarning(" Audio Clip is not working");
            }

            Attack(); 
        }
    }

    void Attack()
    {
        
        animator.SetTrigger("Attack");

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
        {
            Debug.Log("Raycast hit: " + hit.collider.name);

            if (hit.collider.CompareTag("Enemy"))
            {
                EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage();
                }
            }
        }

    }

   
   


}
