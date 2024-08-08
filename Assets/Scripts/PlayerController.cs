using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Speed at which the player moves
    public GameObject bulletPrefab; // Prefab for the bullet
    public Transform bulletSpawnPoint; // Transform representing the spawn point for bullets
    public Vector2 minBounds; // Minimum bounds of the game area
    public Vector2 maxBounds; // Maximum bounds of the game area
    public int maxHealth = 10; // Maximum health
    private int currentHealth; // Current health
    public TextMeshProUGUI gameOverText; // UI Text for Game Over
    public TextMeshProUGUI creditsText; // UI Text for Credits
    private int credits = 2; // Starting credits
    public GameObject gameOverPanel; // Reference to the Game Over Panel
    public GameObject continuePanel; // Reference to the Continue Panel
    public TextMeshProUGUI continueText; // Reference to the Continue Text
    private bool isGameOver = false;
    private float continueTimer = 5f;

    void Start()
    {
        // Ensure Rigidbody2D component is set to not use gravity
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0;
        }

        // Initialize health and UI
        currentHealth = maxHealth;
        gameOverText.gameObject.SetActive(false);
        UpdateCreditsText();
    }

    void Update()
    {
        // Get input from the player
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Calculate movement vector and move the player
        Vector2 move = new Vector2(moveX, moveY) * speed * Time.deltaTime;
        transform.Translate(move);

        // Clamp the player's position to ensure it stays within the bounds
        Vector3 clampedPosition = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            Mathf.Clamp(transform.position.y, minBounds.y, maxBounds.y),
            transform.position.z
        );
        transform.position = clampedPosition;

        // Check if the space key is pressed to shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Player attempting to shoot.");
            ShootBullet();
        }

        //Game Over Logic
        if (isGameOver)
        {
            continueTimer -= Time.deltaTime;
            continueText.text = "Continue? " + Mathf.Ceil(continueTimer).ToString("0:00");
            if (continueTimer <= 0)
            {
                ShowContinueScreen();
            }
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
            Debug.LogError("Bullet prefab or bullet spawn point is not assigned in the PlayerController script.");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (credits > 0)
        {
            credits--;
            UpdateCreditsText();
            currentHealth = maxHealth;
            // Reset player position and state
        }
        else
        {
            StartCoroutine(ShowGameOver());
        }
    }

    IEnumerator ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        isGameOver = true;
        yield return new WaitForSeconds(5f); // Wait for 5 seconds
        gameOverPanel.SetActive(false);
        ShowContinueScreen();
    }

    void ShowContinueScreen()
    {
        gameOverPanel.SetActive(false);
        continuePanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    public void OnContinueButton()
    {
        continuePanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
                             // Reset player state
    }

    public void OnQuitButton()
    {
        Application.Quit(); // Quit the game
    }

    private void UpdateCreditsText()
    {
        creditsText.text = "Credits: " + credits;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EnemyBullet"))
        {
            Destroy(collision.gameObject); // Destroy the bullet on collision
            TakeDamage(1); // Adjust damage amount as needed
        }
    }
}
