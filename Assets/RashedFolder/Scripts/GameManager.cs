using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Win Condition Settings")]
    public int enemiesToKill = 3;
    public int coinsToCollect = 3;

    private int enemiesKilled = 0;
    private int coinsCollected = 0;

    [Header("UI Elements")]
    [SerializeField] GameObject winUI;   
    [SerializeField] GameObject gameOverUI;
    [SerializeField] private TextMeshProUGUI progressText;

    private bool gameEnded = false;


    void Start()
    {
        UpdateProgressText();

        if (winUI != null)
            winUI.SetActive(false); // hides Win UI at start

        if (gameOverUI != null)
            gameOverUI.SetActive(false); // (optional) hides Game Over UI too

    }




    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnemyKilled()
    {
        enemiesKilled++;
        UpdateProgressText();
        CheckWinCondition();
    }

    public void CoinCollected()
    {
        coinsCollected++;
        UpdateProgressText();
        CheckWinCondition();
    }

    private void UpdateProgressText()
    {
        if (progressText != null)
        {
            progressText.text = $"Coins: {coinsCollected}/{coinsToCollect} | Enemies: {enemiesKilled}/{enemiesToKill}";
        }
    }


    private void CheckWinCondition()
    {
        if (!gameEnded && enemiesKilled >= enemiesToKill && coinsCollected >= coinsToCollect)
        {
            WinGame();
        }
    }

    public void WinGame()
    {
        gameEnded = true;
        Time.timeScale = 0f;
        if (winUI != null) winUI.SetActive(true);
        Debug.Log("You Win!");
    }

    public void LoseGame()
    {
        if (!gameEnded)
        {
            gameEnded = true;
            Time.timeScale = 0f;
            if (gameOverUI != null) gameOverUI.SetActive(true);
            Debug.Log("Game Over");
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
