using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f; // Speed at which the player moves
    public GameObject bulletPrefab; // Prefab for the bullet
    public Transform bulletSpawnPoint; // Transform representing the spawn point for bullets

    void Start()
    {
        // Ensure Rigidbody2D component is set to not use gravity
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.gravityScale = 0;
        }
    }

    void Update()
    {
        // Get input from the player
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        // Calculate movement vector and move the player
        Vector2 move = new Vector2(moveX, moveY) * speed * Time.deltaTime;
        transform.Translate(move);

        // Check if the space key is pressed to shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate a bullet at the spawn point
        if (bulletPrefab != null && bulletSpawnPoint != null)
        {
            Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Debug.Log("Bullet fired at: " + Time.time);
        }
    }
}
