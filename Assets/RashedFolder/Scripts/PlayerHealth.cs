using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public TextMeshProUGUI healthText;
    public float maxHealth = 100f;
    public float currentHealth;
    public float damageRate = 10f;
    public float regenRate = 20f;

    [SerializeField] GameObject gameOverUI;

    private bool isDead = false;

    void Start()
    {
        currentHealth = maxHealth;
        gameOverUI.SetActive(false);
        UpdateHealthText();
    }

    void Update()
    {
        if (isDead) return;

        if (currentHealth < maxHealth)
            currentHealth += regenRate * Time.deltaTime;

        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        UpdateHealthText();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthText()
    {
        if (healthText != null)
        {
            healthText.text = "Health: " + Mathf.CeilToInt(currentHealth).ToString();  // Regenarates health
        }
    }

    void Die()
    {
        isDead = true;
        GameManager.Instance.LoseGame();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
