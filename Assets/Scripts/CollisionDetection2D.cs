using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection2D : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the bullet collided with an enemy
        if (collision.CompareTag("Enemy"))
        {
            // Destroy the enemy and the bullet
            Debug.Log("Enemy destroyed at: " + Time.time);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
