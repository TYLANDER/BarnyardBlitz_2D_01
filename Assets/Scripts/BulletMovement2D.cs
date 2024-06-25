using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement2D : MonoBehaviour
{
    public float speed = 10f; // Speed at which the bullet moves

    void Start()
    {
        Debug.Log("Bullet created at: " + Time.time);
    }

    void Update()
    {
        // Move the bullet upwards
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        // Destroy the bullet if it moves off screen
        if (transform.position.y > 6f)
        {
            Debug.Log("Bullet destroyed for moving off screen at: " + Time.time);
            Destroy(gameObject);
        }
    }
}
