using UnityEngine;
using UnityEngine.UI;

// Feature: Player health
// Flow: CheckEnemyReachedPlayer -> ApplyDamage -> UpdateHealthBar -> Health <= 0? -> GameOver
//
// This script owns the player's health value and UI. EnemyFollowPath calls
// ApplyDamage() directly when an enemy collides with the player, so the
// "CheckEnemyReachedPlayer" step in the flowchart lives on the enemy side
// as a collision check rather than being polled here every frame.
public class PlayerHealth : MonoBehaviour
{
    [Header("Health")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("UI")]
    public Slider healthBar; // assign a UI Slider in the inspector

    void Awake()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    // Called by EnemyFollowPath when an enemy reaches the player
    public void ApplyDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Max(currentHealth, 0);

        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            GameOver();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.maxValue = maxHealth;
            healthBar.value = currentHealth;
        }
    }

    void GameOver()
    {
        // TODO: hook up game over screen, stop enemy spawning, etc.
        Debug.Log("Game over - player health reached zero");
    }
}