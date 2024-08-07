using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int scoreValue = 100; // Default score value
    private bool isHitByPlayer = false;

    public GameObject enemyBulletPrefab; // Prefab for the enemy bullet
    public float initialShootDelay = 0.5f; // Initial delay before shooting
    public float shootInterval = 2f; // Time interval between shots
    protected Transform player; // Reference to the player
    private float shootTimer; // Timer to keep track of shooting intervals
    public float speed = 1f; // Speed of the enemy

    protected virtual void Start()
    {
        // Find the player object by tag
        player = GameObject.FindGameObjectWithTag("Player").transform;
        shootTimer = initialShootDelay; // Initialize the shoot timer with a shorter delay
        Debug.Log("EnemyBase Start: Initialized and ready to shoot.");
        Debug.Log("Assigned player: " + (player != null ? player.name : "None")); // Log player assignment
    }

    protected virtual void Update()
    {
        // Move the enemy downwards
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Decrease the shoot timer
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0.1f && shootTimer > 0f)
        {
            Debug.Log("Shoot timer about to reach zero: " + shootTimer);
        }

        // If the shoot timer reaches zero, shoot and reset the timer
        if (shootTimer <= 0f)
        {
            Debug.Log("Enemy attempting to fire.");
            FireBullet();
            shootTimer = shootInterval; // Reset the shoot timer
            Debug.Log("Shoot timer reset to: " + shootTimer);
        }
    }

    protected virtual void FireBullet()
    {
        Debug.Log("FireBullet method called.");
        // Instantiate a bullet at the enemy's position
        if (enemyBulletPrefab != null)
        {
            GameObject bullet = Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
            bullet.layer = LayerMask.NameToLayer("EnemyBullet"); // Set bullet to EnemyBullet layer
            Debug.Log("Bullet instantiated.");

            // Hard-code the direction to be strictly downward
            Vector2 direction = Vector2.down;

            // Set the bullet's velocity downward
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = direction * 5f; // Adjust speed as necessary
                Debug.Log("Bullet velocity set downward.");
            }
            else
            {
                Debug.LogError("Bullet does not have a Rigidbody2D component.");
            }
        }
        else
        {
            Debug.LogError("Bullet prefab is not assigned in the EnemyBase script.");
        }
    }


    // Method to call when the enemy is hit by a player bullet
    public void HitByPlayer()
    {
        isHitByPlayer = true;
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
}
