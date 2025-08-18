using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private GameObject deathUI;

    private int currentHealth;
    private bool isDead = false;

    private void Start()
    {
        currentHealth = maxHealth;
        Time.timeScale = 1f; // Start with normal time
        if (deathUI != null)
            deathUI.SetActive(false);
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;

        currentHealth -= amount;
        Debug.Log($"Player took {amount} damage. Health: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        Debug.Log("Player died.");

        if (deathUI != null)
            deathUI.SetActive(true); // Show death screen

        Time.timeScale = 0f; //  Instantly freeze time
      
       Cursor.lockState = CursorLockMode.None;
       Cursor.visible = true;
    }
}