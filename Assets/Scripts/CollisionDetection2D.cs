using UnityEngine;

public class CollisionDetection2D : MonoBehaviour
{
    public GameObject explosionPrefab; // Reference to the explosion prefab
    private bool isHit = false; // To prevent multiple triggers

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with a bullet and the enemy hasn't been hit before
        if (collision.CompareTag("Bullet") && !isHit)
        {
            isHit = true;
            Debug.Log("Enemy hit at: " + Time.time);

            // Instantiate the explosion at the enemy's position
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Debug.Log("Explosion instantiated at: " + Time.time);

            // Destroy the enemy and the bullet
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
