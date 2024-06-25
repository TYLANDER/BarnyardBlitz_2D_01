using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement2D : MonoBehaviour
{
    public float speed = 2f; // Speed at which the enemy moves
    private Vector2 moveDirection; // Direction in which the enemy moves

    void Start()
    {
        // Generate a random move direction
        moveDirection = new Vector2(Random.Range(-0.5f, 0.5f), -1).normalized;
    }

    void Update()
    {
        // Move the enemy in the specified direction
        transform.Translate(moveDirection * speed * Time.deltaTime);

        // Destroy the enemy if it moves off screen
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
