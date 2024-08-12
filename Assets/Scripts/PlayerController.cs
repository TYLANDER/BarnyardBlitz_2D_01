using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public UIManager uiManager; // Reference to the UIManager to manage UI transitions
    public float speed = 5f; // Speed at which the player moves
    public GameObject bulletPrefab; // Prefab for the bullet the player will shoot
    public Transform bulletSpawnPoint; // Position from where the bullet will be spawned
    public Vector2 minBounds; // Minimum bounds of the game area to restrict player movement
    public Vector2 maxBounds; // Maximum bounds of the game area to restrict player movement
    public int maxHealth = 10; // Maximum health of the player
    public int currentHealth; // Current health of the player


    void Start()
    {
        // Called when the script instance is first loaded
        Debug.Log("PlayerController script started.");

        // Ensure Rigidbody2D component is set to not use gravity (important for 2D games)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0; // Set gravity scale to 0 so the player doesn't fall
        }

        // Initialize player's health to maximum health at the start of the game
        currentHealth = maxHealth;

    }

    void Update()
    {
        // Handle player movement based on input
        float moveX = Input.GetAxis("Horizontal"); // Get horizontal input (A/D keys or Arrow keys)
        float moveY = Input.GetAxis("Vertical"); // Get vertical input (W/S keys or Arrow keys)

        // Calculate the movement vector based on input and speed
        Vector2 move = new Vector2(moveX, moveY) * speed * Time.deltaTime;
        transform.Translate(move); // Apply the movement to the player

        // Clamp the player's position to stay within the specified bounds
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y),
            transform.position.z
        );
        transform.position = clampedPosition; // Set the player's position to the clamped position

        // Check if the space key is pressed to shoot a bullet
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player attempting to shoot.");
            ShootBullet(); // Call the method to shoot a bullet
        }
    }

    void ShootBullet()
    {
        // Instantiate a bullet at the spawn point
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Debug.Log("Bullet fired at: " + Time.time);
        }
        else
        {
            // Log an error if the bullet prefab or spawn point is not assigned
            Debug.LogError("Bullet prefab or bullet spawn point is not assigned in the PlayerController script.");
        }
    }

    public void TakeDamage(int amount)
    {
        // Called when the player takes damage
        Debug.Log("Player took damage: " + amount + " at time: " + Time.time);
        currentHealth -= amount; // Subtract the damage amount from the player's current health
        Debug.Log("Player current health: " + currentHealth);

        if (currentHealth <= 0)
        {
            // If health drops to 0 or below, trigger the Die method
            Debug.Log("Player health reached 0. Triggering Die() method.");
            Die(); // Call the method to handle player's death
        }
    }

    protected void Die()
    {
        // Handle the player's death
        Debug.Log("Die method called.");
        uiManager.ShowGameOverPanel(); // Show the Game Over panel through the UIManager
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Handle trigger collisions, especially with enemy bullets
        Debug.Log("Trigger detected with: " + other.gameObject.name);
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            Debug.Log("Player hit by enemy bullet.");
            Destroy(other.gameObject); // Destroy the bullet on collision
            TakeDamage(1); // Reduce player's health by 1 (or another amount if needed)
        }
    }
}
