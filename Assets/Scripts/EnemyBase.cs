using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int scoreValue = 100; // Default score value
    private bool isHitByPlayer = false;

    // Method to call when the enemy is hit by a player bullet
    public void HitByPlayer()
    {
        isHitByPlayer = true;
        // Add logic to reduce health or destroy the enemy
        // Example:
        // health -= damageAmount;
        // if (health <= 0) Destroy(gameObject);
    }
    void OnDestroy()
    {
        if (isHitByPlayer && ScoreManager.instance != null)
        {
            ScoreManager.instance.AddScore(scoreValue, this.GetType().Name);
        }
        else if (ScoreManager.instance != null)
        {
            ScoreManager.instance.ResetChain();
        }
    }

    // Common enemy functionalities can be added here
}
